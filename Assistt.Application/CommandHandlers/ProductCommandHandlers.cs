using Assistt.Application.Commands;
using Assistt.Domain.Models;
using Assistt.Infrastructure.Repositories;
using Assistt.Infrastructure.UnitOfWork;
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

        private readonly IUnitOfWork _unitOfWork;

        public ProductCommandHandlers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Task<int> Handle(ProductCommands.CreateProduct request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name
            };

            _unitOfWork.Products.Create(product);
            _unitOfWork.Commit();

            return Task.FromResult(product.Id);
        }

    }
}
