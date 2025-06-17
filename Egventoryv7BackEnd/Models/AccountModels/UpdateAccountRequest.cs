namespace Egventoryv7BackEnd.Models.AccountModels
{
    public class UpdateAccountRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
