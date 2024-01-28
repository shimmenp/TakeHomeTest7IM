using Newtonsoft.Json;
using TakeHomeTest7IM.Models;

namespace TakeHomeTest7IM.Services
{
    public class DataService : IDataService
    {
        private readonly string _filePath;
        public DataService(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<PersonDto> GetPersons()
        {
            string jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<PersonDto>>(jsonData);
        }
    }
}
