using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class RoleController : BaseApiController
    {
        RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet("roles")]
        public IReadOnlyList<AppRole> GetRoles()
        {
            var roles = _roleManager.Roles;
            return roles.ToList();
        }
        [HttpPost]
        public async Task<ActionResult<IdentityResult>> CreateRole(string name, string type)
        {
            var role = new AppRole
            {
                Name = name,
                RoleType = type
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
    }
}