using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Commands;

namespace Products.Tests.CommandTests
{
    [TestClass]
    public class ActiveProductCommandTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ActiveProductCommand _commandWithInvalidId;
        private readonly ActiveProductCommand _commandWithValidId;

        public ActiveProductCommandTests()
        {
            _commandWithInvalidId = _fixture
                                    .Build<ActiveProductCommand>()
                                    .Without(x => x.Id)
                                    .Create();

            _commandWithValidId = _fixture
                                                .Build<ActiveProductCommand>()
                                                .With(x => x.Id, Guid.NewGuid())
                                                .Create();
        }

        [TestMethod]
        public void Active_Product_Without_Id()
        {
            _commandWithInvalidId.Validate();

            Assert.AreEqual(_commandWithInvalidId.Valid, false);
        }

        [TestMethod]
        public void Active_Product_With_Valid_Id()
        {
            _commandWithValidId.Validate();

            Assert.AreEqual(_commandWithValidId.Valid, true);
        }
    }
}
