using System.Threading.Tasks;
using Flunt.Notifications;
using Products.Domain.Commands;
using Products.Domain.Commands.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Handlers.Interfaces;
using Products.Domain.Repositories;

namespace Products.Domain.Handlers
{
    public class ProductHandler : Notifiable,
    IProductHandler
    {
        private readonly IProductRepository _repository;

        public ProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> HandleAsync(CreateProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult<ProductEntity>(false, command.Notifications);

            var product = new ProductEntity(command.Name, command.Price);

            await _repository.CreateAsync(product);
            return new GenericCommandResult<ProductEntity>(true, product);
        }

        public async Task<ICommandResult> HandleAsync(UpdateProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult<ProductEntity>(false, command.Notifications);

            var product = await _repository.GetAsync(command.Id);

            product.SetName(command.Name);
            product.SetPrice(command.Price);

            await _repository.UpdateAsync(product);

            return new GenericCommandResult<ProductEntity>(true, product);
        }

        public async Task<ICommandResult> HandleAsync(ActiveProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult<ProductEntity>(false, command.Notifications);

            var product = await _repository.GetAsync(command.Id);

            product.Active();

            await _repository.UpdateAsync(product);

            return new GenericCommandResult<ProductEntity>(true, product);
        }

        public async Task<ICommandResult> HandleAsync(DeleteProductCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult<ProductEntity>(false, command.Notifications);

            var product = await _repository.GetAsync(command.Id);

            product.Delete();

            await _repository.UpdateAsync(product);

            return new GenericCommandResult<ProductEntity>(true, product);
        }
    }
}