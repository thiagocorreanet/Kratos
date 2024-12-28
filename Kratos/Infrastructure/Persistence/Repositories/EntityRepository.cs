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
        
        public async Task<IEnumerable<Entity>> GetAllProjectRelAsync()
        {
            return await _context.Set<Entity>()
                .Include(e => e.ProjectRel) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity>> GetEntitiesByProjectIdAsync(int projectId)
        {
            return await _context.Set<Entity>()
                .Where(e => e.ProjectId == projectId)
                .ToListAsync();
        }
    }
}