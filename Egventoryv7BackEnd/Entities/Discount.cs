using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string DiscountName { get; set; }
        public double DiscountRate { get; set; }

        [JsonIgnore]
        public List<SalesItem> SalesItems { get; set; }
    }
}
