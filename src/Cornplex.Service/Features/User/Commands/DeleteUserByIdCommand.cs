namespace Cornplex.Service.Features.User.Commands
{
    using Cornplex.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteUserByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
            {
                var User = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.Id);
                if (User == null) return default;

                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                return User.Id;
            }
        }
    }
}
