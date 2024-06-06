using MotoOpinion.Models;

namespace SendEmail.Services
{
    public interface IEmailService
    {

        void SendEmail(EmailDTO request);
    }
}
