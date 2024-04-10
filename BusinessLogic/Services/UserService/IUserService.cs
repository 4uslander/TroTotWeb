using DataAccess.ViewModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.UserService
{
    public interface IUserService
    {
        Task<string> LoginAsync(UserLoginFormModel model);
        Task<string> SignupAsync(UserSignupFormModel model);
        Task<ViewUser> GetByUserIdAsync(int userId);
        Task CreateUserAsync(string token, UserFormModel model);
        Task EditUserAsync(UserFormModel model, int userId);
        Task InactiveUserAsync(string token, int userId);
        Task<IList<ViewUser>> GetUsersAsync(string token);
    }
}
