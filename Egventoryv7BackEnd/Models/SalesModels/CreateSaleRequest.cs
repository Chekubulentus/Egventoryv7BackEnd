using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Models.SalesModels
{
    public class CreateSaleRequest
    {
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public int EmployeeId { get; set; }
        public double TotalAmount { get; set; }
        public List<SalesItem> SalesItems { get; set; }
    }
}
