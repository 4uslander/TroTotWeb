using BusinessLogic.Services.TypeService;
using DataAccess.ViewModels.Types;
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
    public class TypesController : ControllerBase
    {
        private ITypeService _typeService;

        public TypesController(ITypeService typeService) {
            _typeService = typeService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<ViewType>>> GetAllTypes()
        {
            try
            {
                var types = await _typeService.GetAllTypesAsync();
                return Ok(types);
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
