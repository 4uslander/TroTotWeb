using Microsoft.AspNetCore.Mvc;

namespace TroTotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {

        public BlogsController() { }

        // GET: api/Blogs
        //[HttpGet]
        //public async Task<ActionResult<IList<Blog>>> GetBlogs()
        //{
        //    var blogs = blogRepo.GetBlogs();
        //    return Ok(blogs);
        //}

        //// GET: api/Blogs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Blog>> GetBlog(int id)
        //{
        //    var blog = blogRepo.GetBlog(id);
        //    if (blog == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(blog);
        //}

        //// PUT: api/Blogs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBlog(int id, Blog blog)
        //{
        //    if (id != blog.BlogId)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        repo.Insert(blog);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BlogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return Ok();
        //}

        //// POST: api/Blogs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        //{
        //    try
        //    {
        //        repo.Insert(blog);
        //    }catch(Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    return Ok(blog);
        //}

        //// DELETE: api/Blogs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBlog(int id)
        //{
        //    var blog = repo.GetById(id);
        //    if (blog == null)
        //    {
        //        return NotFound();
        //    }
        //    repo.Delete(id);
        //    return Ok();
        //}

        //private bool BlogExists(int id)
        //{
        //    var blog = repo.GetById(id);
        //    if(blog == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
