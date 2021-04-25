namespace Cornplex.Service.Features.User.Commands
{
    using Cornplex.Persistence;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;

    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var User = new User();
                User.Email = request.Email;
                User.FirstName = request.FirstName;
                User.LastName = request.LastName;
                User.IsActive = request.IsActive;

                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                return User.Id;
            }
        }
    }
}
