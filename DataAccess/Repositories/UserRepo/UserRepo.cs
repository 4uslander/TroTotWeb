using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Repositories.GenericRepo;
using DataAccess.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.Enum;
using System;

namespace DataAccess.Repositories.UserRepo
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(TroTotDBContext context) : base(context)
        {
        }

        public async Task<UserTokenViewModel> GetByEmailAndPassword(string email, string password)
        {
            var query = from u in context.Users join r in context.Roles on u.RoleId equals r.RoleId
                        where u.Email.Equals(email) && u.Password.Equals(password) select new { u, r };
            return await query.Select(selector => new UserTokenViewModel()
            {
                UserId = selector.u.UserId,
                FullName = selector.u.FullName,
                Email = selector.u.Email,
                Role = selector.r.RoleName,
                Status = (UserStatus)selector.u.Status,
            }).FirstOrDefaultAsync();
        }

        public async Task CreateUserForSignup(UserSignupFormModel model)
        {
            var roleId = await context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(UserRole.User.ToString()));
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                RoleId = roleId.RoleId,
                Status = (int)UserStatus.Active
            };
            await CreateAsync(user);
        }

        public async Task<bool> CheckEmailAndPassword(string email, string password)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
            return (user != null) ? true : false;
        }

        public async Task<bool> IsExistedEmail(string email)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            return (user != null) ? true : false;
        }

        public async Task UpgradeRole(int userId)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));
            Role role = await context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals("Vip"));
            user.RoleId = role.RoleId;
            await UpdateAsync(user);
        }

        public async Task<UserTokenViewModel> GetByUserId(int userId)
        {
            var query = from u in context.Users
                        join r in context.Roles on u.RoleId equals r.RoleId
                        where u.UserId.Equals(userId)
                        select new { u, r };
            return await query.Select(selector => new UserTokenViewModel()
            {
                UserId = selector.u.UserId,
                FullName = selector.u.FullName,
                Email = selector.u.Email,
                Role = selector.r.RoleName,
                Status = (UserStatus)selector.u.Status,
            }).FirstOrDefaultAsync();
        }

        public async Task<ViewUser> GetUserByUserId(int userId)
        {
            var query = from u in context.Users
                        join r in context.Roles on u.RoleId equals r.RoleId
                        where u.UserId.Equals(userId)
                        select new { u, r };
            ViewUser user =  await query.Select(selector => new ViewUser()
            {
                UserId = selector.u.UserId,
                Bio = selector.u.Bio,         
                FullName = selector.u.FullName,
                Email = selector.u.Email,
                Description = selector.u.Description,
                Image = selector.u.Image,
                PhoneNumber = selector.u.PhoneNumber,
                DateOfBirth = selector.u.DateOfBirth,
                DateOfBirthString = selector.u.DateOfBirth.ToString(),
                Status = (UserStatus)selector.u.Status,
                StatusString = (selector.u.Status.Equals((int) UserStatus.Active)) ? "Đang hoạt động" : "Đã vô hiệu hóa",
                RoleId = selector.u.RoleId,
                RoleName = selector.r.RoleName
            }).FirstOrDefaultAsync();
            return (user != null) ? user : null;
        }

        public async Task CreateUser(UserFormModel model)
        {
            var user = new User
            {
                Bio = model.Bio,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Description = model.Description,
                Image = model.Image,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = DateTime.ParseExact(model.DateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToLocalTime(),
                RoleId = model.RoleId,
                Status = (int)model.Status
            };
            await CreateAsync(user);
        }

        public async Task EditUser(UserFormModel model, int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));
            user.Bio = model.Bio;
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.Description = model.Description;
            user.Image = model.Image;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = DateTime.ParseExact(model.DateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();
            user.RoleId = model.RoleId;
            user.Status = (int)model.Status;
            await UpdateAsync(user);
        }

        public async Task InactiveUser(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));
            user.Status = (int)UserStatus.Inactive;
            await UpdateAsync(user);
        }

        public async Task<IList<ViewUser>> GetUsers()
        {
            var query = from u in context.Users
                        join r in context.Roles on u.RoleId equals r.RoleId
                        select new { u, r };
            IList<ViewUser> users = await query.Select(selector => new ViewUser()
            {
                UserId = selector.u.UserId,
                Bio = selector.u.Bio,
                FullName = selector.u.FullName,
                Email = selector.u.Email,
                Description = selector.u.Description,
                Image = selector.u.Image,
                PhoneNumber = selector.u.PhoneNumber,
                DateOfBirth = selector.u.DateOfBirth,
                DateOfBirthString = selector.u.DateOfBirth.ToString(),
                Status = (UserStatus)selector.u.Status,
                StatusString = (selector.u.Status.Equals((int)UserStatus.Active)) ? "Đang hoạt động" : "Đã vô hiệu hóa",
                RoleId = selector.u.RoleId,
                RoleName = selector.r.RoleName
            }).ToListAsync();
            return (users.Count > 0) ? users : null;
        }
    }
}
