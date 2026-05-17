using System.Net.Mail;
using System.Net;

namespace WebBanSanPhamCongNghe.Helper
{
    public class EmailService
    {
        private IWebHostEnvironment Environment { get; set; }

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            Environment = environment;
        }

        public string PopulateBody(string OTP)
        {
            //Lấy Template HTML
            string body = string.Empty;
            string path = Path.Combine(this.Environment.WebRootPath, "Template\\EmailTemplate.htm");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", OTP);
            return body;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpConfig = _configuration.GetSection("Smtp");

            using var smtpClient = new SmtpClient(smtpConfig["Host"])
            {
                Port = int.Parse(smtpConfig["Port"]),
                Credentials = new NetworkCredential(smtpConfig["UserName"], smtpConfig["Password"]),
                EnableSsl = bool.Parse(smtpConfig["EnableSsl"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpConfig["FromAddress"], "L Shop"), // Use FromAddress for sender email
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage); 
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw; 
            }
        }
    }
}
