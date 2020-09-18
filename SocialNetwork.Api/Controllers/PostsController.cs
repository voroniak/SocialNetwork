using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Services.Implementation;

namespace SocialNetwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostDto post)
        {
            await _postService.AddPostAsync(post);

            return Created("Add", post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllAsync());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            return Ok(await _postService.GetByUserIdAsync(userId));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(PostEditDto post)
        {
            await _postService.EditAsync(post);

            return NoContent();
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete( string postId)
        {
            await _postService.DeleteAsync(postId);

            return NoContent();
        }
    }
}
