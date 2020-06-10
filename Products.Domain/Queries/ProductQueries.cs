using System;
using System.Linq.Expressions;
using Products.Domain.Entities;

namespace Products.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<ProductEntity, bool>> Get(Guid id)
        {
            return x => x.Id == id;
        }
        public static Expression<Func<ProductEntity, bool>> GetActives()
        {
            return x => !x.Deleted;
        }

        public static Expression<Func<ProductEntity, bool>> GetDeleted()
        {
            return x => x.Deleted;
        }

    }
}