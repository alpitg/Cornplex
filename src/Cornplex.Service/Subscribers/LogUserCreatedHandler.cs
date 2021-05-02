namespace Cornplex.Service.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Service.Models.Events;
    using MediatR;

    public class LogUserCreatedHandler : INotificationHandler<UserCreatedEvent>
    {

        public LogUserCreatedHandler()
        {
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("User Created Notification");
        }
    }
}
