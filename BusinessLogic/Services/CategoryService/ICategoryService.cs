using DataAccess.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IList<ViewCategory>> GetAllCategoriesAsync();
    }
}
