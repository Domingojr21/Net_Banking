using NetBanking.Core.Application.DTOs.Email;
using NetBanking.Core.Domain.Settings;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
