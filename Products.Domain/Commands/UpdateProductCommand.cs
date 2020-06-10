using System;
using Flunt.Notifications;
using Flunt.Validations;
using Products.Domain.Commands.Interfaces;

namespace Products.Domain.Commands
{
    public class UpdateProductCommand : Notifiable, ICommand
    {
        public UpdateProductCommand()
        {

        }

        public UpdateProductCommand(Guid id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public void Validate()
        {
            AddNotifications(
               new Contract()
               .Requires()
               .AreNotEquals(Id.ToString(), "00000000-0000-0000-0000-000000000000", "id", "Id Inválido")
               .HasLen(Id.ToString(), 36, "id", "Id deve ter 36 caracteres")
               .HasMinLen(Name, 3, "name", "Nome do produto deve ter minimo de 3 caracteres")
               .HasMaxLen(Name, 100, "name", "Nome deve ter máximo de 100 caracteres")
               .IsGreaterThan(Price, 0, "price", "Preço deve ser maior que R$ 0")
             );
        }
    }
}