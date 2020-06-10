using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Domain.Commands;

namespace Products.Tests.CommandTests
{
    [TestClass]
    public class DeleteProductCommandTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly DeleteProductCommand _commandWithInvalidId;
        private readonly DeleteProductCommand _commandWithValidId;

        public DeleteProductCommandTests()
        {
            _commandWithInvalidId = _fixture
                                    .Build<DeleteProductCommand>()
                                    .Without(x => x.Id)
                                    .Create();

            _commandWithValidId = _fixture
                                                .Build<DeleteProductCommand>()
                                                .With(x => x.Id, Guid.NewGuid())
                                                .Create();
        }

        [TestMethod]
        public void Delete_Product_Without_Id()
        {
            _commandWithInvalidId.Validate();

            Assert.AreEqual(_commandWithInvalidId.Valid, false);
        }

        [TestMethod]
        public void Delete_Product_With_Valid_Id()
        {
            _commandWithValidId.Validate();

            Assert.AreEqual(_commandWithValidId.Valid, true);
        }
    }
}
