using Flunt.Notifications;
using Flunt.Validations;
using Products.Domain.Commands.Interfaces;

namespace Products.Domain.Commands
{
    public class CreateProductCommand : Notifiable, ICommand
    {
        public CreateProductCommand()
        {
        }

        public CreateProductCommand(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public double Price { get; set; }

        public void Validate()
        {
            AddNotifications(
              new Contract()
              .Requires()
              .HasMinLen(Name, 3, "name", "Nome do produto deve ter minimo de 3 caracteres")
              .HasMaxLen(Name, 100, "name", "Nome deve ter máximo de 100 caracteres")
              .IsGreaterThan(Price, 0, "price", "Preço deve ser maior que R$ 0")
            );
        }
    }
}