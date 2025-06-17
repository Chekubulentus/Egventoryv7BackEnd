namespace Egventoryv7BackEnd.Models.AccountModels
{
    public class CreateAccountRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
    }
}
