
using Microsoft.Extensions.Options;
using SMLYS.ApplicationCore.Interfaces.Base;
using SMLYS.Infrastructure.Configuration.Email;
using System.Threading.Tasks;

namespace SMLYS.Infrastructure.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private SendGridOptions _sendGridOptions { get; }
        private INetcoreService _netcoreService { get; }
        private SmtpOptions _smtpOptions { get; }

        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
            INetcoreService netcoreService,
            IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _netcoreService = netcoreService;
            _smtpOptions = smtpOptions.Value;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            //send email using sendgrid via netcoreService
            _netcoreService.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey,
                _sendGridOptions.FromEmail,
                _sendGridOptions.FromFullName,
                subject,
                message,
                email).Wait();

            //send email using smtp via dotnetdesk. uncomment to use it
            /*
            _dotnetdesk.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                _smtpOptions.fromFullName,
                subject,
                message,
                email,
                email,
                _smtpOptions.smtpUserName,
                _smtpOptions.smtpPassword,
                _smtpOptions.smtpHost,
                _smtpOptions.smtpPort,
                _smtpOptions.smtpSSL).Wait();
                */
            return Task.CompletedTask;
        }
    }
}
