using System.Text.Json.Serialization;

namespace Egventoryv7BackEnd.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
        [JsonIgnore]
        public List<SalesItem> SalesItems { get; set; }
    }
}
