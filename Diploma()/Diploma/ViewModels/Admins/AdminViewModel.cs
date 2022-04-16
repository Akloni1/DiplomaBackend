namespace Diploma.ViewModels.Admins
{
    public class AdminViewModel
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
   //     public string Password { get; set; }
        public string Role { get; set; }
        public int? BoxingClubId { get; set; }
    }
}
