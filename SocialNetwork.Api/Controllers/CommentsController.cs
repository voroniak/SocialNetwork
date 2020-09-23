using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    }
}
