using TakeHomeTest7IM.Models;

namespace TakeHomeTest7IM.Services
{
    public interface ISearchService
    {
        IEnumerable<PersonDto> SearchPersons(string searchTerm);
    }
}
