using Products.Domain.Commands;
using Products.Domain.Commands.Interfaces;

namespace Products.Domain.Handlers.Interfaces
{
    public interface IProductHandler :
    IHandler<CreateProductCommand>,
    IHandler<UpdateProductCommand>,
    IHandler<ActiveProductCommand>,
    IHandler<DeleteProductCommand>
    {

    }
}