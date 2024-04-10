using DataAccess.ViewModels.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.TypeService
{
    public interface ITypeService
    {
        Task<IList<ViewType>> GetAllTypesAsync();
    }
}
