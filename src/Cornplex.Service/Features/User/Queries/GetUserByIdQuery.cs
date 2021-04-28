namespace Cornplex.Service.Features.User.Queries
{
    using Cornplex.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Cornplex.Persistence.IRepositories;

    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IUserRepository _userRepo;
            public GetUserByIdQueryHandler(IUserRepository userRepo)
            {
                _userRepo = userRepo;
            }

            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepo.GetAsync(x => x.Id == request.Id);
                if (user == null) return null;
                return user;

                //var userDto = _userMapper.MapUserDto(user);
                //return userDto;

            }
        }
    }
}
