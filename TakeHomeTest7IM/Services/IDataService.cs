using TakeHomeTest7IM.Models;

namespace TakeHomeTest7IM.Services
{
    public interface IDataService
    {
        IEnumerable<PersonDto> GetPersons();
    }
}
