namespace Egventoryv7BackEnd.Models.SalesItemModels
{
    public class TransacSalesItem
    {
        public int SalesId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public string Discount { get; set; }
    }
}
