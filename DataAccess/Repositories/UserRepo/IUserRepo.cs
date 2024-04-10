using DataAccess.ViewModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepo
{
    public interface IUserRepo
    {
        Task<bool> CheckEmailAndPassword(string email, string password);
        Task<bool> IsExistedEmail(string email);
        Task<UserTokenViewModel> GetByEmailAndPassword(string email, string password);
        Task CreateUserForSignup(UserSignupFormModel model);
        Task UpgradeRole(int userId);
        Task<UserTokenViewModel> GetByUserId(int userId);
        Task<ViewUser> GetUserByUserId(int userId);
        Task CreateUser(UserFormModel model);
        Task EditUser(UserFormModel model, int userId);
        Task InactiveUser(int userId);
        Task<IList<ViewUser>> GetUsers();
    }
}
