using Core.Entities;
using Core.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EntityRepository : BaseRepository<Entity>, IEntityRepository
    {
        public EntityRepository(DbContextProject context) : base(context)
        {
        }

        public async Task<Entity> GetAllEntityByIdAllPropertityAsync(int id)
        {
            var getEntityById = await _dbSet
                .AsNoTracking()
                .Include(x => x.PropertyRel)
                .SingleOrDefaultAsync(x => x.Id == id);

            return getEntityById;
        }
    }
}