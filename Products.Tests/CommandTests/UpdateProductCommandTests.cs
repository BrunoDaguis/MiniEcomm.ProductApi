using System;
using System.Diagnostics;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Commands;

namespace Products.Tests.CommandTests
{
    [TestClass]
    public class UpdateProductCommandTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly UpdateProductCommand _commandWithoutId;
        private readonly UpdateProductCommand _commandWithId;
        private readonly UpdateProductCommand _commandWithoutName;
        private readonly UpdateProductCommand _commandWithName;
        private readonly UpdateProductCommand _commandWithPriceZero;
        private readonly UpdateProductCommand _commandWithPriceGreaterZero;

        public UpdateProductCommandTests()
        {
            _commandWithoutId = _fixture.Build<UpdateProductCommand>()
                                        .Without(x => x.Id)
                                        .Create();

            _commandWithId = _fixture.Build<UpdateProductCommand>()
                                .With(x => x.Id, Guid.NewGuid())
                                .Create();

            _commandWithoutName = _fixture
                                    .Build<UpdateProductCommand>()
                                    .Without(x => x.Name)
                                    .Create();

            _commandWithName = _fixture
                                    .Build<UpdateProductCommand>()
                                    .With(x => x.Name, _fixture.Create<string>())
                                    .Create();


            _commandWithPriceZero = _fixture
                                        .Build<UpdateProductCommand>()
                                        .With(x => x.Price, 0)
                                        .Create();

            _commandWithPriceGreaterZero = _fixture
                                        .Build<UpdateProductCommand>()
                                        .With(x => x.Price, 450)
                                        .Create();
        }

        [TestMethod]
        public void Update_Product_Without_Name()
        {
            _commandWithoutName.Validate();

            Assert.AreEqual(_commandWithoutName.Valid, false);
        }

        [TestMethod]
        public void Update_Product_With_Name()
        {
            _commandWithName.Validate();

            Assert.AreEqual(_commandWithName.Valid, true);
        }

        [TestMethod]
        public void Update_Product_With_Price_Zero()
        {
            _commandWithPriceZero.Validate();

            Assert.AreEqual(_commandWithPriceZero.Valid, false);
        }

        [TestMethod]
        public void Update_Product_With_Price_Greater_Zero()
        {
            _commandWithPriceGreaterZero.Validate();

            Assert.AreEqual(_commandWithPriceGreaterZero.Valid, true);
        }

        [TestMethod]
        public void Update_Product_Without_Id()
        {
            _commandWithoutId.Validate();

            Assert.AreEqual(_commandWithoutId.Valid, false);
        }

        [TestMethod]
        public void Update_Product_With_Id()
        {
            _commandWithId.Validate();

            Assert.AreEqual(_commandWithId.Valid, true);
        }
    }
}
