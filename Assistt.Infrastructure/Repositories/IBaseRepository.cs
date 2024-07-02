using Assistt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        IQueryable<T> GetAllWithPagination(int page, int pageSize);
        T GetById(int id);
        void Create(T entity);
        void Delete(T entity);
    }
}
