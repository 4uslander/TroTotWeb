using DataAccess.ViewModels.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.TypeRepo
{
    public interface ITypeRepo
    {
        Task<IList<ViewType>> GetAllTypes();
    }
}
