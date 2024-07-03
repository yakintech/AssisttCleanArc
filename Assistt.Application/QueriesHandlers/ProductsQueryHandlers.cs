using Assistt.Application.DTO;
using Assistt.Application.Exceptions;
using Assistt.Application.Queries;
using Assistt.Infrastructure.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.QueriesHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<GetAllProductsDto>>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<List<GetAllProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products =  unitOfWork.Products.GetAll();


            if(request.Search != null)
            {
                products = products.Where(x => x.Name.Contains(request.Search));
            }

            //var result = _mapper.Map<List<GetAllProductsDto>>(products);


            var result = new List<GetAllProductsDto>();

           result = products.Select(x => new GetAllProductsDto
           {
                Id = x.Id,
                Name = x.Name,
                AddDate = x.AddDate
            }).ToList();

            return result;
        }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetProductByIdResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = unitOfWork.Products.GetById(request.Id);

            if(product == null)
            {
                throw new DataNotFoundException("Product not found");
            }

            return new GetProductByIdResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                AddDate = product.AddDate
            };
        }
    }

    public class GetPRoductsWithPaginationQueryHandler : IRequestHandler<GetAllProductsWithPaginationQuery, List<GetAllProductsDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public GetPRoductsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductsDto>> Handle(GetAllProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var products = unitOfWork.Products.GetAllWithPagination(request.Page, request.PageSize);

            var result = _mapper.Map<List<GetAllProductsDto>>(products);
            return result;
        }
    }


    public class GetAllProductsWithCategoryQueryHandler : IRequestHandler<GetAllProductsWithCategoryQuery, List<GetAllProductsWithCategoryDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsWithCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductsWithCategoryDto>> Handle(GetAllProductsWithCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = unitOfWork.Products.GetAllWithIncludes("Category");

            var result = new List<GetAllProductsWithCategoryDto>();

            foreach (var product in products)
            {
                var productDto = new GetAllProductsWithCategoryDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.Category?.Name
                };

                result.Add(productDto);
            }
            return result;
        }
    }



}
