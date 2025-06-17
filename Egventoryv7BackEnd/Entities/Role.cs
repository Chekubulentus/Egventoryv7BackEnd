using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RolePosition { get; set; }


        [JsonIgnore]
        public List<Employee> Employees { get; set; }
    }
}
