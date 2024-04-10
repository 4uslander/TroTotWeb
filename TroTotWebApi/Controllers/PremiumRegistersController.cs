using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Context;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Services.PremiumRegisterService;

namespace TroTotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PremiumRegistersController : ControllerBase
    {
        private IPremiumRegisterService _premiumRegisterService;

        public PremiumRegistersController(IPremiumRegisterService premiumRegisterService)
        {
            _premiumRegisterService = premiumRegisterService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPremium([FromQuery] int premiumType, int userId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _premiumRegisterService.RegisterPremiumAsync(token, premiumType, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
