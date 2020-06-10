using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Entities;
using Products.Domain.Queries;

namespace Products.Tests.QueryTests
{
    [TestClass]
    public class ProductQueryTests
    {
        private readonly Fixture _fixture = new Fixture();
        private List<ProductEntity> _items;

        public ProductQueryTests()
        {
            var p1 = _fixture.Create<ProductEntity>();
            var p2 = _fixture.Create<ProductEntity>();
            var p3 = _fixture.Create<ProductEntity>();
            var p4 = _fixture.Create<ProductEntity>();
            var p5 = _fixture.Create<ProductEntity>();

            p1.Delete();
            p2.Delete();
            p3.Active();
            p4.Active();
            p5.Delete();

            _items = new List<ProductEntity>()
            {
                p1,
                p2,
                p3,
                p4,
                p5
            };
        }

        [TestMethod]
        public void Product_Query_Return_Actives()
        {
            var result = _items.AsQueryable().Where(ProductQueries.GetActives());

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Product_Query_Return_Deleteds()
        {
            var result = _items.AsQueryable().Where(ProductQueries.GetDeleted());

            Assert.AreEqual(3, result.Count());
        }
    }
}