using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class EntityRepository(DbContextProject context) : BaseRepository<Entity>(context), IEntityRepository;
}