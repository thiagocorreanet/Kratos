using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EntityRepository(DbContextProject context) : BaseRepository<Entity>(context), IEntityRepository
    {
        public async Task<Entity?> GetAllPropertiesAsync(int id)
        {
                var entity = await _dbSet
                    .AsNoTracking()
                    .Include(x => x.PropertyRel)  
                    .SingleOrDefaultAsync(x => x.Id == id);

                return entity;
        }
    }
}