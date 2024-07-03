using Assistt.Infrastructure.EF;
using Assistt.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AssisttContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(AssisttContext context, IProductRepository productRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public IProductRepository Products => _productRepository;
        public ICategoryRepository Categories => _categoryRepository;

        public IUserRepository Users => _userRepository;

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
