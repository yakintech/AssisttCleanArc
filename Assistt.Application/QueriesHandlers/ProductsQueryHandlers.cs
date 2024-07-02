using Assistt.Application.DTO;
using Assistt.Application.Queries;
using Assistt.Infrastructure.UnitOfWork;
using AutoMapper;
using MediatR;
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

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<List<GetAllProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products =  unitOfWork.Products.GetAll();

            var result = _mapper.Map<List<GetAllProductsDto>>(products);
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
}
