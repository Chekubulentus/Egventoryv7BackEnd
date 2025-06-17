using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public int EmployeeId { get; set; }
        public double TotalAmount { get; set; }
        public List<SalesItem> SalesItems { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
