using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TypeDataRepository : BaseRepository<TypeData>, ITypeDataRepository
{
    public TypeDataRepository(DbContextProject context) : base(context)
    {
    }
    
    public async Task<IEnumerable<TypeData>> GetAllOrderedByNameAsync()
    {
        return await _context.Set<TypeData>()
            .OrderBy(td => td.Name)
            .ToListAsync();
    }
    
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<TypeData>()
            .AnyAsync(td => td.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}