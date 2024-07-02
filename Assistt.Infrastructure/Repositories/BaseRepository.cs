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

        public virtual IQueryable<T> GetAll()
        {
           var result = dbSet.Where(x => x.IsDeleted == false);
           return result;
        }

        public T GetById(int id)
        {
            var result = dbSet.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return result;
        }

        public IQueryable<T> GetAllWithPagination(int page, int pageSize)
        {
            var result = dbSet.Where(x => x.IsDeleted == false).Skip((page - 1) * pageSize).Take(pageSize);
            return result;
        }


        public IQueryable<T> GetAllWithIncludes(params string[] includes)
        {
            var query = dbSet.Where(x => x.IsDeleted == false).AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }


        public List<T> GetAllWithQuery(Func<T, bool> query)
        {
            var result = dbSet.Where(x => x.IsDeleted == false).Where(query).ToList();
            return result;
        }


    }
}
