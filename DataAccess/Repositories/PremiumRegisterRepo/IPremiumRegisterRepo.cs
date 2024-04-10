using System.Threading.Tasks;

namespace DataAccess.Repositories.PremiumRegisterRepo
{
    public interface IPremiumRegisterRepo
    {
        Task RegisterPremium(int premiumType, int userId);
        Task<bool> IsRegistered(int userId);
    }
}
