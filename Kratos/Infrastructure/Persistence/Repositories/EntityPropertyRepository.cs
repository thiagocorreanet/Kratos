using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EntityPropertyRepository : BaseRepository<EntityProperty>, IEntityPropertyRepository
{
    public EntityPropertyRepository(DbContextProject context) : base(context) { }

    public async Task<IEnumerable<EntityProperty>> GetAllEntitiesPropertiesByEntityIdAsync(int id)
    {
        return await _context.Set<EntityProperty>()
            .Where(e => e.EntityId == id)
            .Include(e => e.EntityRel)   
            .Include(e => e.TypeDataRel) 
            .ToListAsync();
    }
    
    public async Task<IEnumerable<EntityProperty>> GetAllTypeDataAsync()
    {
        return  await _context.Set<EntityProperty>()
            .Include(e => e.TypeDataRel)
            .Include(e => e.EntityRel)
            .ToListAsync();
    }
}
