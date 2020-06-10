using System.Collections.Generic;
using Flunt.Notifications;
using Products.Domain.Commands.Interfaces;
using Products.Domain.Entities;

namespace Products.Domain.Commands
{
    public class GenericCommandResult<T> : ICommandResult where T : Entity
    {
        public GenericCommandResult()
        {
        }

        public GenericCommandResult(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public GenericCommandResult(bool success, IReadOnlyCollection<Notification> notifications)
        {
            Success = success;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public IReadOnlyCollection<Notification> Notifications { get; set; }
    }
}