using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PremiumRegisterRepo
{
    public class PremiumRegisterRepo : GenericRepo<PremiumRegister>, IPremiumRegisterRepo
    {
        public PremiumRegisterRepo(TroTotDBContext context) : base(context)
        {
        }

        public async Task RegisterPremium(int premiumType, int userId)
        {
            var startTime = DateTime.Now.ToLocalTime();
            var endTime = DateTime.Now.ToLocalTime();
            switch (premiumType)
            {
                case 1:
                    endTime = endTime.AddDays(30).ToLocalTime();
                    break;
                case 2:
                    endTime = endTime.AddDays(365).ToLocalTime();
                    break;
                default:
                    break;
            }
            var premiumRegister = new PremiumRegister()
            {
                UserId = userId,
                PremiumTypeId = premiumType,
                StartTime = startTime,
                EndTime= endTime,
            };
            await CreateAsync(premiumRegister);
        }

        public async Task<bool> IsRegistered(int userId)
        {
            var user = await context.PremiumRegisters.FirstOrDefaultAsync(pr => pr.UserId.Equals(userId));
            return (user != null) ? true : false;
        }
    }
}
