using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Entities;
using Projekt_Inżynierski.Entities.Dtos;
using Projekt_Inżynierski.Interfaces;

namespace Projekt_Inżynierski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController( UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAdress);

            if(user == null)
                return Unauthorized("Email adress not found or wrong password !");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Email adress not found or wrong password !");

            return Ok(
                new NewUserToReturnDto
                {
                    GivenName = user.GivenName,
                    EmailAdress = user.Email,
                    Token = _tokenService.CreateJWTToken(user)
                }
            );
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    GivenName = registerDto.GivenName,
                    Email = registerDto.EmailAddress,
                    UserName = registerDto.EmailAddress
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Client");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserToReturnDto
                            {
                                GivenName = appUser.GivenName,
                                EmailAdress = appUser.Email,
                                Token = _tokenService.CreateJWTToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return BadRequest("Faild to add a Role !");
                    }
                }
                else
                {
                    return BadRequest("User with this Email adress already exists or password format wrong !");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}