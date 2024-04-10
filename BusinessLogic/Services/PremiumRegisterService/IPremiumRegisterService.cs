using System.Threading.Tasks;

namespace BusinessLogic.Services.PremiumRegisterService
{
    public interface IPremiumRegisterService
    {
        Task RegisterPremiumAsync(string token, int premiumType, int userId);
    }
}
