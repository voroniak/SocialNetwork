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
    public class LikesController : ControllerBase
    {
        private readonly LikeService _likeService;

        public LikesController(LikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(LikePostDto likePostDto)
        {
            await _likeService.AddAsync(likePostDto);

            return Created("Add", likePostDto);
        }

        [HttpDelete("likeId")]
        public async Task<IActionResult> Delete(string likeId)
        {
            await _likeService.DeleteAsync(likeId);

            return NoContent();
        }

    }
}
