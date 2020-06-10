using System;
using Flunt.Notifications;
using Flunt.Validations;
using Products.Domain.Commands.Interfaces;

namespace Products.Domain.Commands
{
    public class DeleteProductCommand : Notifiable, ICommand
    {
        public DeleteProductCommand()
        {

        }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
              new Contract()
                .Requires()
                .AreNotEquals(Id.ToString(), "00000000-0000-0000-0000-000000000000", "id", "Id Inválido")
                .HasLen(Id.ToString(), 36, "id", "Id deve ter 36 caracteres")
            );
        }
    }
}