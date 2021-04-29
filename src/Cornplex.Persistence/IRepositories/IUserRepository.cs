namespace Cornplex.Persistence.IRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(int id);
    }
}
