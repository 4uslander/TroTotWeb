using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Repositories.GenericRepo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.ViewModels.Types;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TypeRepo
{
    public class TypeRepo : GenericRepo<Type>, ITypeRepo
    {
        public TypeRepo(TroTotDBContext context) : base(context)
        {
        }

        public async Task<IList<ViewType>> GetAllTypes()
        {
            var query = from t in context.Types select t;
            IList<ViewType> items = await query.Select(selector => new ViewType
            {
                TypeId = selector.TypeId,
                TypeName = selector.TypeName
            }).ToListAsync();
            return (items.Count > 0) ? items : null;
        }
    }
}
