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
    public class CreateProductHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IProductRepository _productRepository;
        private readonly IProductHandler _handler;
        private readonly CreateProductCommand _commandWithoutName;
        private readonly CreateProductCommand _commandWithName;
        private readonly CreateProductCommand _commandWithPriceZero;
        private readonly CreateProductCommand _commandWithPriceGreaterZero;

        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new ProductHandler(_productRepository);

            _commandWithoutName = _fixture
                                    .Build<CreateProductCommand>()
                                    .Without(x => x.Name)
                                    .Create();

            _commandWithName = _fixture
                                    .Build<CreateProductCommand>()
                                    .With(x => x.Name, _fixture.Create<string>())
                                    .Create();

            _commandWithPriceZero = _fixture
                                        .Build<CreateProductCommand>()
                                        .With(x => x.Price, 0)
                                        .Create();

            _commandWithPriceGreaterZero = _fixture
                                        .Build<CreateProductCommand>()
                                        .With(x => x.Price, 450)
                                        .Create();
        }

        [TestMethod]
        public async Task Create_Product_Without_Name()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithoutName);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Create_Product_With_Name()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithName);
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public async Task Create_Product_With_Price_Zero()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithPriceZero);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Create_Product_With_Price_Greater_Zero()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithPriceGreaterZero);
            Assert.AreEqual(result.Success, true);
        }
    }
}
