using TakeHomeTest7IM.Models;

namespace TakeHomeTest7IM.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDataService _dataService;

        public SearchService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<PersonDto> SearchPersons(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<PersonDto>();
            }

            searchTerm = searchTerm.ToLower();

            var persons = _dataService.GetPersons();

            return persons.Where(person =>
                (person.FirstName.ToLower() + " " + person.LastName.ToLower()).Contains(searchTerm) ||
                person.Email.ToLower().Contains(searchTerm)
            );
        }
    }
}
