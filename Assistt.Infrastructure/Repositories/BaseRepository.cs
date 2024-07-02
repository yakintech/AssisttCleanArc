using Assistt.Domain.Models;
using Assistt.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        internal AssisttContext context;
        internal DbSet<T> dbSet;

        public BaseRepository(AssisttContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public void Create(T entity)
        {
            entity.AddDate = DateTime.Now;
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public virtual List<T> GetAll()
        {
           var result = dbSet.Where(x => x.IsDeleted == false).ToList();
           return result;
        }

        public T GetById(int id)
        {
            var result = dbSet.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return result;
        }

    }
}
