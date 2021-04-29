namespace Cornplex.Persistence.Repositories.Cached
{
    using System;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using Cornplex.Domain.Enum;
    using Cornplex.Persistence.IRepositories;
    using Microsoft.Extensions.Caching.Memory;

    public class CachedUserRepository : Repository<User>, IUserRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache;

        public CachedUserRepository(ApplicationDbContext dbContext, IUserRepository userRepository, IMemoryCache memoryCache) : base(dbContext)
        {
            _userRepository = userRepository;
            _cache = memoryCache;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User userDetail = new User();
            var key = CacheKeys.UserId + "_" + id;

            if (_cache.TryGetValue(key, out userDetail))
            {
                return userDetail;
            }

            userDetail = await _userRepository.GetByIdAsync(id).ConfigureAwait(false);

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
            _cache.Set(key, userDetail, cacheEntryOptions);

            return userDetail;
        }
    }
}
