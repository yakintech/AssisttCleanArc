using Assistt.Application.Commands;
using Assistt.Domain.Models;
using Assistt.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.CommandHandlers
{
    public class ProductCommandHandlers : IRequestHandler<ProductCommands.CreateProduct, int>
    {

        private readonly IProductRepository _productRepository; 
        public ProductCommandHandlers(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public Task<int> Handle(ProductCommands.CreateProduct request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name
            };

            _productRepository.CreateProduct(product);

            return Task.FromResult(product.Id);
        }

    }
}
