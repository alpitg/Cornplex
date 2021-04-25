namespace Cornplex.Persistence
{
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();
    }
}