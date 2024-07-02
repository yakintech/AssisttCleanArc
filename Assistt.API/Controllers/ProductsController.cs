using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assistt.Application.DTO;
using Assistt.Application.Commands;
using MediatR;

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
    }
}
