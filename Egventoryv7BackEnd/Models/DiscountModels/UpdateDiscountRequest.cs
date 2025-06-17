namespace Egventoryv7BackEnd.Models.DiscountModels
{
    public class UpdateDiscountRequest
    {
        public int Id { get; set; }
        public string DiscountName { get; set; }
        public double DiscountRate { get; set; }
    }
}
