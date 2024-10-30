
using Core.Entities;

namespace Core.Repositories
{
    public interface IEntityRepository : IBaseRepository<Entity>
    {
        Task<Entity?> GetAllPropertiesAsync(int id);
    }
}