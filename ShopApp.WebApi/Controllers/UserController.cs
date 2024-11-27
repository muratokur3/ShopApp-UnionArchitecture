using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.UserDtos;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.DataAccess.Identity;

namespace ShopApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userService.CreateUserAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.PasswordSignInAsync(model.Username, model.Password);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return Ok();
        }

        [HttpPost("addToRole")]
        public async Task<IActionResult> AddToRole([FromBody] UserRoleDto model)
        {
            var user = await _userService.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userService.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("removeFromRole")]
        public async Task<IActionResult> RemoveFromRole([FromBody] UserRoleDto model)
        {
            var user = await _userService.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userService.RemoveFromRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRoles(string id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userService.GetRolesAsync(user);
            return Ok(roles);
        }
    }
}