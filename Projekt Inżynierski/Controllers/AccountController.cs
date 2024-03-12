using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_Inżynierski.Dtos;
using Projekt_Inżynierski.Entities;
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
        private readonly IMapper _mapper;

        public AccountController( UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager,
         IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentlyLogedUser()
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var logedUser = await _userManager.FindByEmailAsync(emailAddress);

            var userRoles = await _userManager.GetRolesAsync(logedUser);
            string rolesString = string.Join(",", userRoles);

            return Ok(
                new UserToReturnDto
                {
                    Token = _tokenService.CreateJWTToken(logedUser, rolesString),
                    GivenName = logedUser.GivenName,
                    EmailAddress = logedUser.Email
                }
            );
        }

        [HttpGet("email-address-exists")]
        public async Task<IActionResult> EmailAddressExistsChecker([EmailAddress] string emailAdress)
        {
            if( await _userManager.FindByEmailAsync(emailAdress) != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Authorize]
        [HttpGet("shipping-address")]
        public async Task<IActionResult> GetShippingAddressForUser()
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(u => u.ShippingAddress).SingleOrDefaultAsync(e => e.Email == emailAddress);
            
            if(user.ShippingAddress != null)
            {
                var ShippingAddressDto = _mapper.Map<ShippingAddressDto>(user.ShippingAddress);
                return Ok(ShippingAddressDto);
            }
            return BadRequest("Shipping adress empty !");
        }

        [Authorize]
        [HttpPut("shipping-address")]
        public async Task<IActionResult> UpdateShippingAddress(ShippingAddressDto shippingAddress)
        {
            var emailAddress = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(u => u.ShippingAddress).SingleOrDefaultAsync(e => e.Email == emailAddress);

            user.ShippingAddress = _mapper.Map<ShippingAddress>(shippingAddress);

            var state = await _userManager.UpdateAsync(user);

            if(state.Succeeded)
            {
                return Ok( _mapper.Map<ShippingAddressDto>(user.ShippingAddress) );
            }
            return BadRequest("Something went wrong !");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if(user == null)
                return Unauthorized("Email adress not found or wrong password !");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Email adress not found or wrong password !");

            var userRoles = await _userManager.GetRolesAsync(user);
            string rolesString = string.Join(",", userRoles);

            return Ok(
                new UserToReturnDto
                {
                    Token = _tokenService.CreateJWTToken(user, rolesString),
                    GivenName = user.GivenName,
                    EmailAddress = user.Email
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
                            new UserToReturnDto
                            {
                                Token = _tokenService.CreateJWTToken(appUser, "Client"),
                                GivenName = appUser.GivenName,
                                EmailAddress = appUser.Email
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