using JuntoTechnicalTest.Common.Dto;
using JuntoTechnicalTest.IdentityServer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JuntoTechnicalTest.IdentityServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;  

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
            => Ok(await _userService.CreateUser(createUserDto));

        
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
            => Ok(await _userService.ChangePassword(changePasswordDto));
    }
}
