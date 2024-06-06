using MotoOpinion.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MotoOpinion.Models;


namespace SendEmail.Services
{
    namespace SendEmail.Services
    {
        public class EmailService : IEmailService
        {

            private readonly IConfiguration _config;

            public EmailService()
            {
            }

            public EmailService(IConfiguration config)
            {
                _config = config;
            }


            public void SendEmail(EmailDTO request)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(builder.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(request.Para));
                email.Subject = request.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = request.Contenido
                };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    builder.GetSection("Email:Host").Value,
                   Convert.ToInt32(builder.GetSection("Email:Port").Value),
                   SecureSocketOptions.StartTls
                    );


                smtp.Authenticate(builder.GetSection("Email:UserName").Value, builder.GetSection("Email:PassWord").Value);

                smtp.Send(email);
                smtp.Disconnect(true);


            }

        }
    }
}