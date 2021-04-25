namespace Cornplex.Service.Features.User.Queries
{
    using Cornplex.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IApplicationDbContext _context;
            public GetUserByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var User = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.Id);
                if (User == null) return null;
                return User;
            }
        }
    }
}
