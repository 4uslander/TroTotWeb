using BusinessLogic.Utils;
using DataAccess.JWT;
using DataAccess.Repositories.PremiumRegisterRepo;
using DataAccess.Repositories.UserRepo;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PremiumRegisterService
{
    public class PremiumRegisterService : IPremiumRegisterService
    {
        private IPremiumRegisterRepo _premiumRegisterRepo;
        private IUserRepo _userRepo;
        private DecodeToken _decodeToken;

        public PremiumRegisterService(IPremiumRegisterRepo premiumRegisterRepo, IUserRepo userRepo) {
            _premiumRegisterRepo = premiumRegisterRepo;
            _userRepo = userRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task RegisterPremiumAsync(string token, int premiumType, int userId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("User") || role.Equals("Vip")) throw new UnauthorizedAccessException("You do not have permission to do this action!");
                await _premiumRegisterRepo.RegisterPremium(premiumType, userId);
                await _userRepo.UpgradeRole(userId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
