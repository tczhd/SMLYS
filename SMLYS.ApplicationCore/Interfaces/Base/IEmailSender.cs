using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Interfaces.Base
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
