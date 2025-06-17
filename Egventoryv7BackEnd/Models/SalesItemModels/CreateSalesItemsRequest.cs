using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Models.SalesItemModels
{
    public class CreateSalesItemsRequest
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
