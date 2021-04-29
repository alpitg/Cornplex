namespace Cornplex.Persistence.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using Cornplex.Persistence.IRepositories;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
