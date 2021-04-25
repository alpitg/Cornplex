
namespace Cornplex.Service.Features.User.Commands
{
    using Cornplex.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.Id);

                if (user == null)
                {
                    return default;
                }
                else
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.IsActive = request.IsActive;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return user.Id;
                }
            }
        }
    }
}
