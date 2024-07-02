using Assistt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
    }
}
