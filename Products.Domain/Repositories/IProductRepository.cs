using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.Entities;

namespace Products.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductEntity entity);
        Task UpdateAsync(ProductEntity entity);
        Task<ProductEntity> GetAsync(Guid id);
        Task<IEnumerable<ProductEntity>> GetActivesAsync();
        Task<IEnumerable<ProductEntity>> GetDeletedAsync();
    }
}