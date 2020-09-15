using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManagerService.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return BadRequest("Login-NotRegistered");
                }
                //else
                //{
                //    if (!await _authService.IsEmailConfirmedAsync(user))
                //    {
                //        return BadRequest(_resourceForErrors["Login-NotConfirmed"]);
                //    }
                //}
                var result = await _userManagerService.SignInAsync(loginDto);
                if (result.IsLockedOut)
                {
                    return BadRequest("Account-Locked");
                }
                //if (result.Succeeded)
                //{
                //    var generatedToken = await _jwtService.GenerateJWTTokenAsync(user);
                //    return Ok(new { token = generatedToken });
                //}
                //else
                //{
                //    return BadRequest(_resourceForErrors["Login-InCorrectPassword"]);
                //}
            }
            return Ok();
        }
    }
}
