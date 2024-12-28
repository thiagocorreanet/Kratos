using Core.Entities;

namespace Core.Repositories;

public interface ITypeDataRepository : IBaseRepository<TypeData>
{
    Task<IEnumerable<TypeData>> GetAllOrderedByNameAsync();
    Task<bool> ExistsByNameAsync(string name);
}