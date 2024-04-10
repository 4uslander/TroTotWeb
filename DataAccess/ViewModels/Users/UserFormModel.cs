using DataAccess.Enum;

namespace DataAccess.ViewModels.Users
{
    public class UserFormModel
    {
        public string? Bio { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public UserStatus Status { get; set; }
        public int RoleId { get; set; }
    }
}
