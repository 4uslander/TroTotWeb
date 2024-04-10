using BusinessObject.Context;
using BusinessObject.Models;
using DataAccess.Repositories.GenericRepo;
using DataAccess.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(TroTotDBContext context) : base(context)
        {           
        }

        public async Task<IList<ViewCategory>> GetAllCategories()
        {
            var query = from c in context.Categories select c;
            IList<ViewCategory> items = await query.Select(selector => new ViewCategory
            {
                CategoryId = selector.CategoryId,
                CategoryName = selector.CategoryName
            }).ToListAsync();
            return (items.Count > 0) ? items : null;
        }
    }
}
