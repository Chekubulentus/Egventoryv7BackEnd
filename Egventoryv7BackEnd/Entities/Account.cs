using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
