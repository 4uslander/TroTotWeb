using BusinessLogic.Services.CategoryService;
using DataAccess.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TroTotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<ViewCategory>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (NullReferenceException) 
            { 
                return Ok(new List<object>()); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
