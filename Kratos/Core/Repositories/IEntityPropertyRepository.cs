using Core.Entities;

namespace Core.Repositories;

public interface IEntityPropertyRepository : IBaseRepository<EntityProperty>
{
    Task<IEnumerable<EntityProperty>> GetAllTypeDataAsync();
}