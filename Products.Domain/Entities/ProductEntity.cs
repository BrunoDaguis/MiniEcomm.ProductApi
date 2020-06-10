using System;

namespace Products.Domain.Entities
{
    public class ProductEntity : Entity
    {
        public ProductEntity(string name, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Deleted = false;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public bool Deleted { get; private set; }

        public void SetName(string name)
        {
            this.Name = name;
        }
        public void SetPrice(double price)
        {
            this.Price = price;
        }
        public void Delete()
        {
            this.Deleted = true;
        }
        public void Active()
        {
            this.Deleted = false;
        }
    }
}