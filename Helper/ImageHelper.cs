namespace WebBanSanPhamCongNghe.Helper
{
    public class ImageHelper
    {
        public static string UpLoadImage(IFormFile Image, string folder)
        {
            try
            {
                var FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Image.FileName;
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folder, FileName);
                using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Image.CopyTo(myFile);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }
    }
}
