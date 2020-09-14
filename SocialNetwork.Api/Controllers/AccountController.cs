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
    public class AccountController : ControllerBase
    {
        private UserManagerService _userManagerService;

        public AccountController(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            
            return Ok(await _userManagerService.CreateUserAsync(registerDto));
        }
    }
}
