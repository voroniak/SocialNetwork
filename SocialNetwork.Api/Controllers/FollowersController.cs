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
    public class FollowersController : ControllerBase
    {
        private readonly FollowerService _followerService;

        public FollowersController(FollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpPost("Follow")]
        public async Task<IActionResult> FollowUser(FollowDto followDto)
        {
            await _followerService.FollowUserAsync(followDto.FollowerUserId, followDto.FollowingUserId);
            
            return NoContent();
        }

        [HttpPost("Unfollow")]
        public async Task<IActionResult> UnfollowUser(FollowDto followDto)
        {
            await _followerService.UnfollowUserAsync(followDto.FollowerUserId, followDto.FollowingUserId);

            return NoContent();
        }

        [HttpGet("Followers")]
        public async Task<IActionResult> GetAllUserFollowers(string userId)
        {
           return Ok(await _followerService.GetAllUserFollowers(userId));
        }

        [HttpGet("Following")]
        public async Task<IActionResult> GetAllUserFollowing(string userId)
        {
          return Ok(  await _followerService.GetAllUserFollowing(userId));
        }
    }
}
