using FluentAssertions;
using TakeHomeTest7IM.Models;
using TakeHomeTest7IM.Services;

namespace TakeHomeTest7IM.UnitTests.Services
{
    public class DataServiceTests
    {
        [Fact]
        public void GetData_ReturnsData_WhenValidPath()
        {
            // Arrange
            var validPath = "../../testData.json";
            var expectedData = new List<PersonDto>
            {
                new PersonDto("Antony", "Fitt", "afitt0@a8.net", "Male")
            };

            var service = new DataService(validPath);

            // Act
            var result = service.GetPersons();

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedData);
        }

        [Fact]
        public void GetData_ThrowsException_WhenInvalidPath()
        {
            // Act
            var invalidPath = "invalidPath";
            var service = new DataService(invalidPath);

            // Assert
            service.Invoking(s => s.GetPersons()).Should().Throw<FileNotFoundException>();
        }
    }
}
