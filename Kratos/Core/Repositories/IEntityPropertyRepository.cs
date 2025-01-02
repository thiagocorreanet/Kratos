using Core.Entities;

namespace Core.Repositories;

public interface IEntityPropertyRepository : IBaseRepository<EntityProperty>
{
    Task<IEnumerable<EntityProperty>> GetAllEntitiesPropertiesByEntityIdAsync(int id);
    Task<IEnumerable<EntityProperty>> GetAllTypeDataAsync();
}