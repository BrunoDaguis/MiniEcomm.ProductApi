using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Entities;

namespace Products.Tests.EntityTests
{
    [TestClass]
    public class ProductEntityTests
    {
        protected readonly Fixture _fixture = new Fixture();
        private readonly ProductEntity _product;

        public ProductEntityTests()
        {
            _product = _fixture.Create<ProductEntity>();
        }

        [TestMethod]
        public void New_Instance_Product_Deleted_Is_False()
        {
            Assert.AreEqual(_product.Deleted, false);
        }

        [TestMethod]
        public void Set_Product_With_Deleted()
        {
            _product.Delete();

            Assert.AreEqual(_product.Deleted, true);
        }

        [TestMethod]
        public void Set_Product_With_Active()
        {
            _product.Active();

            Assert.AreEqual(_product.Deleted, false);
        }

        [TestMethod]
        public void Set_Product_New_Price()
        {
            ProductEntity newProduct = _fixture.Create<ProductEntity>();

            double newPrice = newProduct.Price;
            _product.SetPrice(newPrice);

            Assert.AreEqual(_product.Price, newPrice);
        }

        [TestMethod]
        public void Set_Product_New_Name()
        {
            ProductEntity newProduct = _fixture.Create<ProductEntity>();

            string newName = newProduct.Name;
            _product.SetName(newName);

            Assert.AreEqual(_product.Name, newName);
        }
    }
}