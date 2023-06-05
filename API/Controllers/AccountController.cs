using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Identity;
using API.Errors;
using API.Extensions;
using API.Helpers;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);

            return new UserDto
            {
                Email = user.UserName,
                Token = await _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
            // var user = await _userManager.FindByNameAsync(loginUser.EmployeeCode);
            // if (user == null || await _userManager.CheckPasswordAsync(user, loginUser.Password))
            //     return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid User!" });
            // var signingCredentials = _jwtHandler.GetSigningCredentials();
            // var claims = _jwtHandler.GetClaims(user);
            // var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            // var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            // return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}