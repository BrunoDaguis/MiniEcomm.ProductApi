using System.Threading.Tasks;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Products.Domain.Commands;
using Products.Domain.Entities;
using Products.Domain.Handlers;
using Products.Domain.Handlers.Interfaces;
using Products.Domain.Repositories;

namespace Products.Tests.HandlerTests
{
    [TestClass]
    public class DeleteProductHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IProductRepository _productRepository;
        private readonly IProductHandler _handler;
        private readonly DeleteProductCommand _commandWithoutId;
        private readonly DeleteProductCommand _commandWithId;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new ProductHandler(_productRepository);

            var productValid = _fixture.Create<ProductEntity>();
            _productRepository.GetAsync(productValid.Id).Returns(productValid);

            _commandWithoutId = _fixture
                                    .Build<DeleteProductCommand>()
                                    .Without(x => x.Id)
                                    .Create();

            _commandWithId = _fixture
                                    .Build<DeleteProductCommand>()
                                    .With(x => x.Id, productValid.Id)
                                    .Create();

        }

        [TestMethod]
        public async Task Delete_Product_Without_Id()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithoutId);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Delete_Product_With_Id()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithId);
            Assert.AreEqual(result.Success, true);
        }
        [TestMethod]
        public async Task Delete_Product_Valid()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithId);

            var product = (ProductEntity)result.Data;
            Assert.AreEqual(product.Deleted, true);
        }
    }
}