using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{    public class SalesItem
    {
        [Key]
        public int Id { get; set; }
        public int SalesId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double Price { get; set; } 
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public string Discount { get; set; }

        [JsonIgnore]
        public Sales Sales { get; set; }
    }

}
