using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Products.Domain.Commands;
using Products.Domain.Entities;
using Products.Domain.Handlers.Interfaces;
using Products.Domain.Repositories;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ProductEntity>> Get([FromServices] IProductRepository repository)
        {
            var products = await repository.GetActivesAsync();

            return products;
        }

        [HttpGet]
        [Route("Deleted")]
        public async Task<IEnumerable<ProductEntity>> GetDeleted([FromServices] IProductRepository repository)
        {
            var products = await repository.GetDeletedAsync();

            return products;
        }

        [HttpPost]
        [Route("")]
        public async Task<GenericCommandResult<ProductEntity>> Create(
           [FromBody] CreateProductCommand command,
           [FromServices] IProductHandler handler)
        {
            var result = (GenericCommandResult<ProductEntity>)await handler.HandleAsync(command);
            return result;
        }

        [HttpPut]
        [Route("")]
        public async Task<GenericCommandResult<ProductEntity>> Update(
            [FromBody] UpdateProductCommand command,
            [FromServices] IProductHandler handler)
        {
            var result = (GenericCommandResult<ProductEntity>)await handler.HandleAsync(command);

            return result;
        }

        [HttpPut]
        [Route("Active")]
        public async Task<GenericCommandResult<ProductEntity>> Active(
                    [FromBody] ActiveProductCommand command,
                    [FromServices] IProductHandler handler)
        {
            var result = (GenericCommandResult<ProductEntity>)await handler.HandleAsync(command);
            return result;
        }

        [HttpDelete]
        [Route("")]
        public async Task<GenericCommandResult<ProductEntity>> Delete(
            [FromBody] DeleteProductCommand command,
            [FromServices] IProductHandler handler)
        {
            var result = (GenericCommandResult<ProductEntity>)await handler.HandleAsync(command);

            return result;
        }
    }
}