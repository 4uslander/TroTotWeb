using BusinessLogic.Services.PostService;
using DataAccess.Enum;
using DataAccess.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TroTotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService) {
            _postService = postService;
        }

        [HttpGet("byId")]
        public async Task<ActionResult<ViewPost>> GetPost([FromQuery] int postId)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(postId);
                return Ok(post);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePost([FromBody] PostFormModel model)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _postService.CreatePostAsync(token, model);
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

        [HttpGet("canCreate")]
        public async Task<ActionResult<bool>> CanCreate()
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                var canCreate = await _postService.CanCreateAsync(token);
                return Ok(canCreate);
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

        [HttpPut("approve")]
        public async Task<IActionResult> ApprovePost([FromQuery] int postId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _postService.ApprovePostAsync(token, postId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IList<ViewPost>>> GetAllPosts([FromQuery] PostRequestModel request)
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync(request);
                return Ok(posts);
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

        [HttpDelete("disable")]
        public async Task<IActionResult> DisablePost([FromQuery] int postId)
        {
            try
            {
                await _postService.DisablePostAsync(postId);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditPost([FromBody] PostFormModel model, [FromQuery] int postId)
        {
            try
            {
                string token = (Request.Headers)["Authorization"].ToString().Split(" ")[1];
                await _postService.EditPostAsync(token, model, postId);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("personal")]
        public async Task<ActionResult<IList<ViewPost>>> GetPersonalPosts([FromQuery] int userId)
        {
            try
            {
                var posts = await _postService.GetPersonalPostsAsync(userId);
                return Ok(posts);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
