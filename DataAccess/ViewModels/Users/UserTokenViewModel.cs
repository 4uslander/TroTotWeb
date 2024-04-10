using DataAccess.Enum;

namespace DataAccess.ViewModels.Users
{
    public class UserTokenViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
