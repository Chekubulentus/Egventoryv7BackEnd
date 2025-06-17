using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Name
        {
            get { return $"{this.LastName}, {this.FirstName}"; }
        }

        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        [JsonIgnore]
        public List<Sales> Sales { get; set; }
    }
}
