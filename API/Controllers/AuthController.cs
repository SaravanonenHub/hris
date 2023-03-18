using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Dtos.Identity;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{

    public class AuthController : BaseApiController
    {
        [HttpPost]
        public IActionResult Login([FromBody] AppUserDto user)
        {
            if (user is null) return BadRequest("Invalid User");

            if (user.EmployeeCode == "3434" && user.Password == "3434")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("leo is bL0ody$weet"));
                var signInCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7114",
                    audience: "https://localhost:7114",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredential
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponse { Token = tokenString });
            }

            return Unauthorized();
        }
    }
}