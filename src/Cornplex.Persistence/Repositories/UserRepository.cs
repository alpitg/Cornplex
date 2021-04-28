namespace Cornplex.Persistence.Repositories
{
    using Cornplex.Domain.Entities;
    using Cornplex.Persistence.IRepositories;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
