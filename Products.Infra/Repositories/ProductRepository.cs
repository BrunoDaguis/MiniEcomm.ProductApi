using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Domain.Queries;
using Products.Domain.Repositories;
using Products.Infra.Context;
using Products.Infra.Context.Settings;

namespace Products.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommDbContext _context;
        public ProductRepository(EcommDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(ProductEntity entity)
        {
            await _context.Products.InsertOneAsync(entity);
        }

        public async Task<ProductEntity> GetAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(ProductQueries.Get(id));
            return product.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductEntity>> GetActivesAsync()
        {
            var products = await _context.Products.FindAsync(ProductQueries.GetActives());
            return products.ToEnumerable();
        }

        public async Task<IEnumerable<ProductEntity>> GetDeletedAsync()
        {
            var products = await _context.Products.FindAsync(ProductQueries.GetDeleted());
            return products.ToEnumerable();
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            var filter = Builders<ProductEntity>.Filter.Eq(x => x.Id, entity.Id);

            var update = Builders<ProductEntity>.Update
                .Set("Name", entity.Name)
                .Set("Price", entity.Price)
                .Set("Deleted", entity.Deleted);

            await _context.Products.UpdateOneAsync(filter, update);
        }
    }
}