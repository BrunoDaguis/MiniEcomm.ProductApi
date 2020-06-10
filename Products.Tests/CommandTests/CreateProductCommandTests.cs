using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Commands;

namespace Products.Tests.CommandTests
{
    [TestClass]
    public class CreateProductCommandTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly CreateProductCommand _commandWithoutName;
        private readonly CreateProductCommand _commandWithName;
        private readonly CreateProductCommand _commandWithPriceZero;
        private readonly CreateProductCommand _commandWithPriceGreaterZero;

        public CreateProductCommandTests()
        {
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
        public void Create_Product_Without_Name()
        {
            _commandWithoutName.Validate();

            Assert.AreEqual(_commandWithoutName.Valid, false);
        }

        [TestMethod]
        public void Create_Product_With_Name()
        {
            _commandWithName.Validate();

            Assert.AreEqual(_commandWithName.Valid, true);
        }

        [TestMethod]
        public void Create_Product_With_Price_Zero()
        {
            _commandWithPriceZero.Validate();

            Assert.AreEqual(_commandWithPriceZero.Valid, false);
        }

        [TestMethod]
        public void Create_Product_With_Price_Greater_Zero()
        {
            _commandWithPriceGreaterZero.Validate();

            Assert.AreEqual(_commandWithPriceGreaterZero.Valid, true);
        }
    }
}
