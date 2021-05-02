namespace Cornplex.Service.Contract
{
    using System.Threading.Tasks;
    using Cornplex.Domain.Settings;

    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
