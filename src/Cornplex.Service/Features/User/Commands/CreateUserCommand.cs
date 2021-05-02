namespace Cornplex.Service.Features.User.Commands
{
    using Cornplex.Persistence;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using System;
    using Cornplex.Service.Models.Events;

    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            public CreateUserCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;

            }
            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User();
                user.Email = request.Email;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.IsActive = request.IsActive;
                user.UpdatedOn = DateTime.UtcNow;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // NOTE: Log & send mail
                await _mediator.Publish(new UserCreatedEvent(request.Email, user.FirstName), cancellationToken);

                return user.Id;
            }
        }
    }
}
