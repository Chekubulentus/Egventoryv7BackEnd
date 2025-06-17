using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.SalesItemModels;

namespace Egventoryv7BackEnd.Models.SalesModels
{
    public class TransactionRequest
    {
        public int EmployeeId { get; set; }
        public List<TransacSalesItem> SalesItems { get; set; }
    }
}
