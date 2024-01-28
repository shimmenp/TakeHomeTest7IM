using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TakeHomeTest7IM.Controllers;
using TakeHomeTest7IM.Models;
using TakeHomeTest7IM.Services;

namespace TakeHomeTest7IM.UnitTests.Controllers
{
    public class SearchControllerTests
    {
        private readonly ILogger<SearchController> _logger;

        public SearchControllerTests()
        {
            var loggerMock = new Mock<ILogger<SearchController>>();
            _logger = loggerMock.Object;
        }

        [Fact]
        public void Search_WithValidSearchTerm_ReturnsOkResult()
        {
            // Arrange
            var searchTerm = "Anthony";
            var expectedPerson = new PersonDto("Anthony", "Fitt", "afitt0@a8.net", "Male");
            var searchServiceMock = new Mock<ISearchService>();
            searchServiceMock.Setup(service => service.SearchPersons(searchTerm))
                             .Returns(new List<PersonDto> { expectedPerson });
            var controller = new SearchController(searchServiceMock.Object, _logger);

            // Act
            var result = controller.Search(searchTerm);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(new List<PersonDto> { expectedPerson });
        }


        [Fact]
        public void Search_WithEmptySearchTerm_ReturnsBadRequest()
        {
            // Arrange
            var searchServiceMock = new Mock<ISearchService>();
            var controller = new SearchController(searchServiceMock.Object, _logger);

            // Act
            var result = controller.Search(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be("Please provide a search term.");
        }

        [Fact]
        public void Search_WithNoResults_ReturnsNotFound()
        {
            // Arrange
            var searchTerm = "Unknown";
            var searchServiceMock = new Mock<ISearchService>();
            searchServiceMock.Setup(service => service.SearchPersons(searchTerm))
                             .Returns(new List<PersonDto>());
            var controller = new SearchController(searchServiceMock.Object, _logger);

            // Act
            var result = controller.Search(searchTerm);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.Value.Should().Be("No results found.");
        }

        [Fact]
        public void Search_WitException_ReturnsInternalServerError()
        {
            // Arrange
            var searchTerm = "James";
            var searchServiceMock = new Mock<ISearchService>();
            searchServiceMock.Setup(service => service.SearchPersons(searchTerm))
                             .Throws(new Exception("Fake exception"));
            var controller = new SearchController(searchServiceMock.Object, _logger);

            // Act
            var result = controller.Search(searchTerm);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            statusCodeResult.StatusCode.Should().Be(500);
        }
    }
}
