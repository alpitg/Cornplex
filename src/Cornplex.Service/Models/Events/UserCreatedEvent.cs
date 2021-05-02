namespace Cornplex.Service.Models.Events
{
    using MediatR;

    public class UserCreatedEvent : INotification
    {
        public string Email { get; }
        public string FirstName { get; }

        public UserCreatedEvent(string email, string firstName)
        {
            Email = email;
            FirstName = firstName;
        }
    }
}