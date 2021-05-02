
namespace Cornplex.Service.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Domain.Settings;
    using Cornplex.Service.Contract;
    using Cornplex.Service.Models.Events;
    using MediatR;
    using Microsoft.FeatureManagement;

    class EmailUserCreatedHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IFeatureManager _featureManager;

        public EmailUserCreatedHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //if (await _featureManager.IsEnabledAsync(nameof(FeatureManagement.EnableEmailService)))
            //{

                MailRequest request = new MailRequest() {
                    From = "mail@cornplex.com",
                    ToEmail = notification.Email,
                    Subject = "Account created successfully",
                    Body = $"<p>Hi {notification.FirstName}  <br /> Welcome to our community. <br /> Thanks </p>"
                };

                await _emailService.SendEmailAsync(request);

                Console.WriteLine("Email sent to the user.");
            //}
            //else
            //{
            //    Console.WriteLine("Mail service is dissabled.");
            //}

        }
    }
}
