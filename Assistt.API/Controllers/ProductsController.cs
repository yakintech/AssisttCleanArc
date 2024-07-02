using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assistt.Application.DTO;
using Assistt.Application.Commands;
using MediatR;
using Assistt.Application.Queries;

namespace Assistt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            var command = new ProductCommands.CreateProduct
            {
                Name = model.Name
            };

            var result = await _mediator.Send(command);

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
                var query = new GetAllProductsQuery
                {
                    Search = search };

                var result = await _mediator.Send(query);
                return Ok(result);
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] int page, [FromQuery] int pageSize)
        //{
        //    var query = new GetAllProductsWithPaginationQuery
        //    {
        //        Page = page,
        //        PageSize = pageSize

        //    };

        //    var result = await _mediator.Send(query);

        //    return Ok(result);
        //}





        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProductByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("GetAllProductsWithCategory")]
        public async Task<IActionResult> GetAllProductsWithCategory()
        {
            var query = new GetAllProductsWithCategoryQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }



    }
}
