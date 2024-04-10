using Microsoft.AspNetCore.Mvc;

namespace TroTotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        public RolesController() { }

        // GET: api/Roles
        //[HttpGet]
        //public async Task<ActionResult<IList<Role>>> GetRoles()
        //{
        //    var roles = roleRepo.GetRoles();
        //    return Ok(roles);
        //}

        //// GET: api/Roles/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Role>> GetRole(int id)
        //{
        //    var role = repo.GetById(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(role);
        //}

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRole(int id, Role role)
        //{
        //    if (id != role.RoleId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(role).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Roles
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Role>> PostRole(Role role)
        //{
        //    _context.Roles.Add(role);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        //}

        //// DELETE: api/Roles/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RoleExists(int id)
        //{
        //    return _context.Roles.Any(e => e.RoleId == id);
        //}
    }
}
