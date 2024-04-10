using DataAccess.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.CategoryRepo
{
    public interface ICategoryRepo
    {
        Task<IList<ViewCategory>> GetAllCategories();
    }
}
