using DataAccess.Enum;
using System;

namespace DataAccess.ViewModels.Users
{
    public class ViewUser
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DateOfBirthString { get; set; }
        public UserStatus Status { get; set; }
        public string StatusString { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
