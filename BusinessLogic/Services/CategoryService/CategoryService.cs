using DataAccess.Repositories.CategoryRepo;
using DataAccess.ViewModels.Categories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IList<ViewCategory>> GetAllCategoriesAsync()
        {
            var items = await _categoryRepo.GetAllCategories();
            if(items == null) throw new NullReferenceException("Not found any categories!");
            return items;
        }
    }
}
