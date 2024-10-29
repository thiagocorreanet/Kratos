using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class EntityPropertyRepository(DbContextProject context)
    : BaseRepository<EntityProperty>(context), IEntityPropertyRepository;
