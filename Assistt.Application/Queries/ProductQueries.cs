using Assistt.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<GetAllProductsDto>>
    {
        public string Search { get; set; }
    }

    public class GetProductByIdQuery : IRequest<GetProductByIdResponseDto>
    {
        public int Id { get; set; }
    }

    public class GetAllProductsWithPaginationQuery : IRequest<List<GetAllProductsDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }


    public class GetAllProductsWithCategoryQuery : IRequest<List<GetAllProductsWithCategoryDto>>
    {

    }


}
