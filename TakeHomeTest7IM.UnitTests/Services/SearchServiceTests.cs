using FluentAssertions;
using Moq;
using TakeHomeTest7IM.Models;
using TakeHomeTest7IM.Services;

namespace TakeHomeTest7IM.UnitTests.Services
{
    public class SearchServiceTests
    {
        // This is the important data to test the specific initial requirements
        private readonly List<PersonDto> _persons = new List<PersonDto>
        {
            new PersonDto("James", "Kubu", "hkubu7@craigslist.org", "Male"),
            new PersonDto("James", "Pfeffer", "bpfeffera@amazon.com", "Male"),
            new PersonDto("Katey", "Soltan", "ksoltanh@simplemachines.org", "Female"),
            new PersonDto("Chalmers", "Longfut", "clongfujam@wp.com", "Male"),
            new PersonDto("Moselle", "Gaize", "mgaizec@tumblr.com", "Female")
        };

        [Fact]
        public void SearchPersons_WithFirstNameSearchTerm_ReturnsMatchingPersons()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(service => service.GetPersons()).Returns(_persons);
            var service = new SearchService(dataServiceMock.Object);

            // Act
            var result = service.SearchPersons("James");

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PersonDto>>();
            var resultPersons = result.As<IEnumerable<PersonDto>>();
            resultPersons.Should().HaveCount(2);
            resultPersons.Should().Contain(person => person.FirstName == "James" && person.LastName == "Kubu");
            resultPersons.Should().Contain(person => person.FirstName == "James" && person.LastName == "Pfeffer");
        }

        [Fact]
        public void SearchPersons_WithPartialSearchTerm_ReturnsMatchingPersons()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(service => service.GetPersons()).Returns(_persons);
            var service = new SearchService(dataServiceMock.Object);

            // Act
            var result = service.SearchPersons("jam");

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PersonDto>>();
            var resultPersons = result.As<IEnumerable<PersonDto>>();
            resultPersons.Should().HaveCount(3);
            resultPersons.Should().Contain(person => person.FirstName == "James" && person.LastName == "Kubu");
            resultPersons.Should().Contain(person => person.FirstName == "James" && person.LastName == "Pfeffer");
            resultPersons.Should().Contain(person => person.FirstName == "Chalmers" && person.LastName == "Longfut");
        }

        [Fact]
        public void SearchPersons_WithFullNameSearchTerm_ReturnsMatchingPerson()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(service => service.GetPersons()).Returns(_persons);
            var service = new SearchService(dataServiceMock.Object);

            // Act
            var result = service.SearchPersons("Katey Soltan");

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<PersonDto>>();
            var resultPersons = result.As<IEnumerable<PersonDto>>();
            resultPersons.Should().HaveCount(1);
            resultPersons.Should().Contain(person => person.FirstName == "Katey" && person.LastName == "Soltan");
        }

        [Fact]
        public void SearchPersons_WithTermThatDoesNotExist_ReturnsNoResults()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(service => service.GetPersons()).Returns(_persons);
            var service = new SearchService(dataServiceMock.Object);

            // Act
            var result = service.SearchPersons("Jasmine Duncan");

            // Assert
            result.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void SearchPersons_WithEmptySearchTerm_ReturnsNoResults()
        {
            // Arrange
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(service => service.GetPersons()).Returns(_persons);
            var service = new SearchService(dataServiceMock.Object);

            // Act
            var result = service.SearchPersons(string.Empty);

            // Assert
            result.Should().NotBeNull().And.BeEmpty();
        }
    }
}
