using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Services;

namespace TechLibrary.Controllers.Tests
{
    [TestFixture()]
    [Category("ControllerTests")]
    public class BooksControllerTests
    {

        private  Mock<ILogger<BooksController>> _mockLogger;
        private  Mock<IBookService> _mockBookService;
        private  Mock<IMapper> _mockMapper;
        private NullReferenceException _expectedException;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = new Mock<ILogger<BooksController>>();
            _mockBookService = new Mock<IBookService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test()]
        public async Task GetAllTest()
        {
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync()).Returns(Task.FromResult(It.IsAny<List<Domain.Book>>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = await sut.GetAll();

            //  Assert
            _mockBookService.Verify(s => s.GetBooksAsync(), Times.Once, "Expected GetBooksAsync to have been called once");
        }
        [Test()]
        public void SaveBook()
        {
            //  Arrange
            int rtn=0;
            BookRequest bookRequest = new BookRequest();
            bookRequest.ISBN = "1933988673";
            bookRequest.PublishedDate = "";
            bookRequest.Title = "Unlocking Android";
            bookRequest.Descr = "test";
            _mockBookService.Setup(b => b.SaveBook(bookRequest)).Returns(rtn);
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);

            //  Act
            var result = sut.SaveBook(bookRequest);

            //  Assert
            _mockBookService.Verify(s => s.SaveBook(bookRequest), Times.Once, "Expected GetBooksAsync to have been called once");
        }
    }
}