
using Core.Entities;

namespace Core.Repositories;

public interface IEntityRepository : IBaseRepository<Entity>
{
    Task<IEnumerable<Entity>> GetEntitiesByProjectIdAsync(int projectId);
    Task<IEnumerable<Entity>> GetAllProjectRelAsync();
}