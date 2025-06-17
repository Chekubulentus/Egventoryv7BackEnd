using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Models.SalesModels
{
    public class SalesDTO
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public string CashierName { get; set; }
        public double TotalAmount { get; set; }
        public List<SalesItem> SalesItems { get; set; }
    }
}
