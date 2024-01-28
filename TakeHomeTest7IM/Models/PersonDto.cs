using Newtonsoft.Json;

namespace TakeHomeTest7IM.Models
{
    public class PersonDto
    {
        [JsonProperty("first_name")]
        public string FirstName { get; }

        [JsonProperty("last_name")]
        public string LastName { get; }

        public string Email { get; }

        public string Gender { get; }

        public PersonDto(string firstName, string lastName, string email, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
        }
    }
}
