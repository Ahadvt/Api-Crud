using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Apps.UserApi.DTOs;
using WebApplication1.Data.Entities;

namespace WebApplication1.Apps.UserApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        //[HttpGet("createrole")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role = await _roleManager.CreateAsync(new IdentityRole("Member"));
        //     role = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    role = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    if (!role.Succeeded)
        //    {
        //        foreach (var item in role.Errors)
        //        {
        //            return Content(item.Description);
        //        }
        //    }
        //    return Ok(role);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtos register)
        {
            AppUser user = await _userManager.FindByNameAsync(register.UserName);

            if (user!=null)
            {
                return BadRequest();
            }
                
              user=  new AppUser
            {
                FullName=register.FullName,
                UserName=register.UserName
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var rolemanager = await _userManager.AddToRoleAsync(user, "Member");
            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDtos login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
                return NotFound();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName)
            };

            var role = await _userManager.GetRolesAsync(user);

            claims.AddRange(role.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            string KeyStr = "edba5df4-0d55-439f-8b86-537612618890";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(KeyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                claims:claims,
                signingCredentials:creds,
                expires:DateTime.Now.AddDays(3),
                issuer: "https://localhost:44384/",
                audience: "https://localhost:44384/"

                );
            string TokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new {token=TokenStr });

          
        }

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Get()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserGetDto userGet = _mapper.Map<UserGetDto>(user);
            return Ok(userGet);
        }

    }
}
