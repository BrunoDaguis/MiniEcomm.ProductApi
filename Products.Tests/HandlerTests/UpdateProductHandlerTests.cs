using System;
using System.Diagnostics;
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
    public class UpdateProductHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IProductRepository _productRepository;
        private readonly IProductHandler _handler;
        private readonly UpdateProductCommand _commandWithoutName;
        private readonly UpdateProductCommand _commandWithName;
        private readonly UpdateProductCommand _commandWithPriceZero;
        private readonly UpdateProductCommand _commandWithPriceGreaterZero;
        private readonly UpdateProductCommand _commandWithInvalidId;
        private readonly UpdateProductCommand _commandWithValidId;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();

            var productValid = _fixture.Create<ProductEntity>();
            _productRepository.GetAsync(productValid.Id).Returns(productValid);

            _handler = new ProductHandler(_productRepository);

            _commandWithoutName = _fixture
                                    .Build<UpdateProductCommand>()
                                    .Without(x => x.Name)
                                    .With(x => x.Id, productValid.Id)
                                    .Create();

            _commandWithName = _fixture
                                    .Build<UpdateProductCommand>()
                                    .With(x => x.Name, _fixture.Create<string>())
                                    .With(x => x.Id, productValid.Id)
                                    .Create();

            _commandWithPriceZero = _fixture
                                        .Build<UpdateProductCommand>()
                                        .With(x => x.Price, 0)
                                        .With(x => x.Id, productValid.Id)
                                        .Create();

            _commandWithPriceGreaterZero = _fixture
                                        .Build<UpdateProductCommand>()
                                        .With(x => x.Price, 450)
                                        .With(x => x.Id, productValid.Id)
                                        .Create();

            _commandWithInvalidId = _fixture
                                        .Build<UpdateProductCommand>()
                                        .Without(x => x.Id)
                                        .Create();

            _commandWithValidId = _fixture
                                        .Build<UpdateProductCommand>()
                                        .With(x => x.Id, productValid.Id)
                                        .Create();
        }

        [TestMethod]
        public async Task Update_Product_Without_Name()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithoutName);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Update_Product_With_Name()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithName);
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public async Task Update_Product_With_Price_Zero()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithPriceZero);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public async Task Update_Product_With_Price_Greater_Zero()
        {
            var result = (GenericCommandResult<ProductEntity>)await _handler.HandleAsync(_commandWithPriceGreaterZero);
            Assert.AreEqual(result.Success, true);
        }
        [TestMethod]
        public void Update_Product_With_Invalid_Id()
        {
            _commandWithInvalidId.Validate();

            Assert.AreEqual(_commandWithInvalidId.Valid, false);
        }

        [TestMethod]
        public void Update_Product_With_Valid_Id()
        {
            _commandWithValidId.Validate();

            Assert.AreEqual(_commandWithValidId.Valid, true);
        }
    }
}
