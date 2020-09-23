using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Services.Implementation;

namespace SocialNetwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;
        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetAllAsync());
        }

        [HttpGet("commentedEntityId")]
        public async Task<IActionResult> GetByCommentedEntityId(string commentedEntityId)
        {
            return Ok(await _commentService.GetByCommentedEnityIdAsync(commentedEntityId));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentPostDto commentPostDto)
        {
            await _commentService.AddAsync(commentPostDto);

            return Created("Add", commentPostDto);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CommentPutDto commentPutDto)
        {
            await _commentService.EditAsync(commentPutDto);

            return NoContent();
        }
    }
}
