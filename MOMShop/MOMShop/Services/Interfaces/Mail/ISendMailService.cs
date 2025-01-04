using MOMShop.Utils.Mail;
using System.Threading.Tasks;

namespace MOMShop.Services.Interfaces.Mail
{
    public interface ISendMailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
