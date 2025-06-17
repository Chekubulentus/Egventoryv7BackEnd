namespace Egventoryv7BackEnd.Models.ProductModels
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
    }
}
