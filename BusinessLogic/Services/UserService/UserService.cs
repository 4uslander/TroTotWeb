using BusinessLogic.Utils;
using DataAccess.Enum;
using DataAccess.JWT;
using DataAccess.Repositories.UserRepo;
using DataAccess.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private DecodeToken _decodeToken;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<string> LoginAsync(UserLoginFormModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentException("Email Null || Password Nulll");
            try
            {
                bool check = await _userRepo.CheckEmailAndPassword(model.Email, model.Password);
                if (!check) throw new ArgumentException("Tài khoản email hoặc mật khẩu không đúng!");
                var user = await _userRepo.GetByEmailAndPassword(model.Email, model.Password);
                if (user.Status.Equals(UserStatus.Inactive)) throw new ArgumentException("Tài khoản của bạn đã bị vô hiệu hóa!");
                return JWTUserToken.GenerateJWTTokenUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SignupAsync(UserSignupFormModel model)
        {
            try
            {
                if (!model.Password.Equals(model.ConfirmPassword)) throw new ArgumentException("Xác nhận mật khẩu không đúng với mật khẩu!");
                var regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";          
                var match = Regex.Match(model.Email, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Email không hợp lệ!");
                var isExisted = await _userRepo.IsExistedEmail(model.Email);
                if (isExisted) throw new ArgumentException("Email đã tồn tại, bạn hãy đăng nhập vào web!");
                regex = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
                match = Regex.Match(model.PhoneNumber, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Số điện thoại không hợp lệ!");
                await _userRepo.CreateUserForSignup(model);
                var user = await _userRepo.GetByEmailAndPassword(model.Email, model.Password);
                return JWTUserToken.GenerateJWTTokenUser(user);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ViewUser> GetByUserIdAsync(int userId)
        {
            var user = await _userRepo.GetUserByUserId(userId);
            if (user == null) throw new NullReferenceException("Not found any users!");
            return user;
        }

        public async Task CreateUserAsync(string token, UserFormModel model)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("User")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                var match = Regex.Match(model.Email, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Email không hợp lệ!");
                var isExisted = await _userRepo.IsExistedEmail(model.Email);
                if (isExisted) throw new ArgumentException("Email đã tồn tại!");
                regex = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
                match = Regex.Match(model.PhoneNumber, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Số điện thoại không hợp lệ!");
                await _userRepo.CreateUser(model);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task EditUserAsync(UserFormModel model, int userId)
        {
            try
            {
                var regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                var match = Regex.Match(model.Email, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Email không hợp lệ!");
                regex = @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
                match = Regex.Match(model.PhoneNumber, regex, RegexOptions.IgnoreCase);
                if (!match.Success) throw new ArgumentException("Số điện thoại không hợp lệ!");
                var user = await _userRepo.GetUserByUserId(userId);
                if (user == null) throw new NullReferenceException("Not found any users!");
                await _userRepo.EditUser(model, userId);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task InactiveUserAsync(string token, int userId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("User")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
                var user = await _userRepo.GetUserByUserId(userId);
                if (user == null) throw new NullReferenceException("Not found any users!");
                await _userRepo.InactiveUser(userId);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<IList<ViewUser>> GetUsersAsync(string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("User")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
            var users = await _userRepo.GetUsers();
            if (users == null) throw new NullReferenceException("Not found any users!");
            return users;
        }
    }
}
