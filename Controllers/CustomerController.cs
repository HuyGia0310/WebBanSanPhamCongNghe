using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Helper;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebBanSanPhamCongNghe.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MyShopContext _context;
        private readonly EmailService _emailService;
        public CustomerController(MyShopContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                // Lấy CustomerId từ Claims
                string? idTam = HttpContext.User.FindFirstValue("CustomerId");

                if (string.IsNullOrEmpty(idTam))
                {
                    return RedirectToAction("SignIn");
                }

                int idCustomer = Convert.ToInt32(idTam);

                // Lấy thông tin Customer từ cơ sở dữ liệu
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == idCustomer);

                if (customer == null)
                {
                    return Redirect("/404");
                }

                var profileView = new Profile 
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Img = customer.Img,
                    DateOfBirth = customer.DateOfBirth,                    
                };
                return View(profileView);
            }
            catch (Exception)
            {
                return Redirect("/404");
            }
        }

        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            string? customerIdStr = HttpContext.User.FindFirstValue("CustomerId");
            if (string.IsNullOrWhiteSpace(customerIdStr))
            {
                return RedirectToAction("SignIn");
            }

            int customerId = Convert.ToInt32(customerIdStr);

            var payments = await _context.Payments
                .AsNoTracking()
                .Include(p => p.Cart)
                .Where(p => p.Cart != null && p.Cart.CustomerId == customerId)
                .OrderByDescending(p => p.CreateAt)
                .ToListAsync();

            return View(payments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDetailCustomer(Profile customer, IFormFile? ImgUpload)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var existingCustomer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == customer.Id);

                    if (ImgUpload != null)
                    {
                        customer.Img = ImageHelper.UpLoadImage(ImgUpload, "Customer");
                    }

                    var updatedCustomer = new Customer
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Address = customer.Address,
                        Phone = customer.Phone,
                        Email = customer.Email,
                        Img = customer.Img,
                        DateOfBirth = customer.DateOfBirth,

                        RegisteredAt = existingCustomer.RegisteredAt,
                        RandomKey = existingCustomer.RandomKey,
                        Password = existingCustomer.Password,
                        Role = existingCustomer.Role,
                        IsActive = existingCustomer.IsActive,

                        UpdateAt = DateTime.Now
                    };

                    _context.Update(updatedCustomer);
                    await _context.SaveChangesAsync();

                    TempData["ProfileSuccessMessage"] = "Cập nhật thông tin thành công";
                    return RedirectToAction("Profile");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Update Customer Fail: {ex}");
                    TempData["ProfileErrorMessage"] = "Lỗi hệ thống";
                    return RedirectToAction("Profile");
                }
            }

            TempData["ProfileErrorMessage"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại!";
            return RedirectToAction("Profile");
        }


        #region SIGN_UP
        // ------------------ SIGN UP --------------------
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignUp customerSignUp, IFormFile? ImgUpload)
        {
            // Kiểm tra tính hợp lệ của model
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem email đã được đăng ký chưa
                    var existingCustomer = await _context.Customers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Email == customerSignUp.Email);

                    // Nếu email đã tồn tại, hiển thị thông báo lỗi và quay lại trang đăng ký
                    if (existingCustomer != null)
                    {
                        TempData["SignupErrorMessage"] = "Email đã được sử dụng.";
                        return RedirectToAction("SignUp", customerSignUp);
                    }


                    //Password và Confirm Password khác nhau
                    if (customerSignUp.Password != customerSignUp.Confirm_NewPassWord)
                    {
                        TempData["SignupErrorMessage"] = "Vui lòng mật khẩu giống nhau";
                        return RedirectToAction("SignUp", customerSignUp);
                    }

                    // Tạo đối tượng Customer mới
                    var now = DateTime.Now;
                    var newCustomer = new Customer
                    {
                        FirstName = customerSignUp.FirstName,
                        LastName = customerSignUp.LastName,
                        Address = customerSignUp.Address,
                        Phone = customerSignUp.Phone,
                        Email = customerSignUp.Email,
                        DateOfBirth = customerSignUp.DateOfBirth,
                        RegisteredAt = now,
                        UpdateAt = now,
                        IsActive = true,
                        Role = 0 // Mặc định là user
                    };

                    // Nếu có hình ảnh được Upload
                    if (ImgUpload != null)
                    {
                        // Upload hình ảnh và lưu đường dẫn
                        newCustomer.Img = ImageHelper.UpLoadImage(ImgUpload, "Customer");
                    }
                    else
                    {
                        // Sử dụng avatar mặc định
                        newCustomer.Img = "avatar-default.jpg";
                    }

                    // Tạo mật khẩu được mã hóa
                    newCustomer.RandomKey = PasswordHelper.GenerateRandomKey();
                    newCustomer.Password = customerSignUp.Password.ToMd5Hash(newCustomer.RandomKey);
                    
                    // Thêm vào cơ sở dữ liệu
                    _context.Add(newCustomer);
                    await _context.SaveChangesAsync();

                    // Hiển thị thông báo thành công
                    TempData["SignupSuccessMessage"] = "Đăng ký thành công!";
                    return RedirectToAction("SignIn"); // Chuyển hướng đến trang đăng nhập
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine($"Lỗi trong quá trình đăng ký: {ex.Message}");
                    TempData["SignupErrorMessage"] = "Có lỗi xảy ra. Vui lòng thử lại sau.";
                    return View(); // Trả lại trang đăng ký với thông báo lỗi
                }
            }

            // Nếu model không hợp lệ, trả lại trang đăng ký với các lỗi xác thực
            return View();
        }

        #endregion

        #region SIGN_IN
        // ------------------ SIGN IN --------------------
        public IActionResult SignIn(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn customerSignIn, string? ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ReturnUrl = ReturnUrl;

                    var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Email == customerSignIn.Email);

                    if (customer == null)
                    {
                        ModelState.AddModelError("Loi", "Không có khách hàng này");
                    }
                    else
                    {
                        if (customer.IsActive == false)
                        {
                            ModelState.AddModelError("Thông báo", "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ Admin");
                        }
                        else
                        {
                            string hashPassword = customerSignIn.Password.ToMd5Hash(customer.RandomKey);
                            if (customer.Password != hashPassword)
                            {
                                ModelState.AddModelError("Thông báo", "Sai mật khẩu");
                            }
                            else
                            {
                                var claims = new List<Claim>
                                {
                                    new Claim("CustomerEmail", customer.Email),
                                    new Claim(ClaimTypes.Name, customer.FirstName),
                                    new Claim("CustomerFirstName", customer.FirstName),
                                    new Claim("CustomerLastName", customer.LastName),
                                    new Claim("CustomerId", customer.Id.ToString()),
                                
                                    //claim -role động   (Cấp quyền)                              
                                    new Claim(ClaimTypes.Role, customer.Role == 1 ? "Administrator" : "Customer"),
                                };

                                var claimIdentity = new ClaimsIdentity(claims, "login");
                                var claimPricipal = new ClaimsPrincipal(claimIdentity);

                                await HttpContext.SignInAsync(claimPricipal);

                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return Redirect("/");
                                }

                            }
                        }
                    }

                    return View();
                }
                else return View();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View();
            }
        }
        #endregion 

        #region SIGN_OUT
        //------------ SIGN OUT -------------
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion

        #region FORGOT_PASSWORD
        //NuGet Package: MailKit

        public IActionResult ForgotPassword()
        {
            //Random 1 chuỗi 6 số ngẫu nhiên
            string randomString = "";
            var rd = new Random();
            for (int i = 0; i < 6; i++)
            {
                randomString = randomString + rd.Next(0, 10).ToString();
            }

            // tạo Model lưu chuỗi random
            CustomerForgotPassword customer = new CustomerForgotPassword();
            customer.RandomCode = randomString;


            return View(customer);
        }

        [HttpPost]
        public IActionResult ForgotPassword(CustomerForgotPassword customerForgot)
        {
            if (customerForgot.RandomCode != customerForgot.OTP)
            {
                return View();
            }
            CustomerNewPassword customer = new CustomerNewPassword();
            customer.Email = customerForgot.Email;

            return RedirectToAction("ResetPassword", customer);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotCheckEmailExistAsync(string email, string otp)
        {
            //Nếu Email Null
            if (email == null)
            {
                return Json("Vui lòng nhập email");
            }

            //Nếu không tim thấy tài khoản nào sử dụng email đã nhập
            Customer? customerCheckMail = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (customerCheckMail == null)
            {
                return Json("Không tìm thấy Email");
            }

            //nếu tìm thấy
            //Gửi Email đã OTP

            string titleMail = "XÁC THỰC PHIÊN GIAO DỊCH ";
            string OTPHtml = otp;

            string body = _emailService.PopulateBody(OTPHtml);
            await _emailService.SendEmailAsync(email, titleMail, body);

            return Json("OK");
        }

        #endregion

        #region Reset_NewPassword

        public IActionResult ResetPassword(CustomerNewPassword customerNewPassword)
        {
            return View(customerNewPassword);
        }

        [HttpPost]
        public IActionResult ResetPasswordPost(CustomerNewPassword customerNewPassword)
        {
            try
            {
                //Password và Confirm Password khác nhau
                if (customerNewPassword.NewPassWord != customerNewPassword.Confirm_NewPassWord)
                {
                    TempData["ResetPasswordErrorMessage"] = "Vui lòng mật khẩu giống nhau";
                    return RedirectToAction("ResetPassword", customerNewPassword);
                }

                //Kiểm tra Email đã được đăng ký tài khoản hay chưa
                Customer? customerExist = _context.Customers.FirstOrDefault(c => c.Email == customerNewPassword.Email);
                if (customerExist == null)
                {
                    TempData["SignUpErrorMessage"] = "Email chưa được đăng ký";
                    return View();
                }

                //HashPassword
                customerNewPassword.RandomKey = PasswordHelper.GenerateRandomKey();
                customerNewPassword.NewPassWord = customerNewPassword.NewPassWord.ToMd5Hash(customerNewPassword.RandomKey);

                customerExist.RandomKey = customerNewPassword.RandomKey;
                customerExist.Password = customerNewPassword.NewPassWord;

                _context.Customers.Update(customerExist);
                var isSuccess = _context.SaveChanges() > 0;

                // Kiểm tra truy vấn SQL thành công hay không?
                if (isSuccess)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Update Password Success");
                    if (HttpContext.User.FindFirstValue("CustomerId") == null)
                    {
                        TempData["SignInSuccessMessage"] = "Cấp lại mật khẩu thành công";
                        return RedirectToAction("SignIn");
                    }
                    TempData["ProfileSuccessMessage"] = "Cấp lại mật khẩu thành công";
                    return RedirectToAction("Profile");
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Update Customer Fail");
                    TempData["SignInSuccessMessage"] = "Lỗi hệ thống";
                    return RedirectToAction("SignIn");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["ResetPasswordErrorMessage"] = ex.Message;
                return RedirectToAction("ResetPassword");
            }
        }
        #endregion
    }
}

