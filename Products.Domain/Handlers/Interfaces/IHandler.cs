using System.Threading.Tasks;
using Products.Domain.Commands.Interfaces;

namespace Products.Domain.Handlers.Interfaces
{
    public interface IHandler<TCommand> where TCommand : ICommand
    {
        Task<ICommandResult> HandleAsync(TCommand command);
    }
}