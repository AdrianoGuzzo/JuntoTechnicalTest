using JuntoTechnicalTest.Common.Dto;
using JuntoTechnicalTest.IdentityServer.Service;
using Microsoft.AspNetCore.Mvc;

namespace JuntoTechnicalTest.IdentityServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto request)
            => Ok(await _userService.CreateUser(request));
    }
}
