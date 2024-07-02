using Assistt.Domain.Models;
using Assistt.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AssisttContext _context;

        public ProductRepository(AssisttContext context)
        {
           _context = context;
        }

        public void CreateProduct(Product product)
        {
           _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
