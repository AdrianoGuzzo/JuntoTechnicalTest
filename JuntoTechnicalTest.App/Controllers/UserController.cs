using JuntoTechnicalTest.App.Services.Interfaces;
using JuntoTechnicalTest.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuntoTechnicalTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IIdentityServer identityServer) : ControllerBase
    {
        private readonly IIdentityServer _identityServer = identityServer;

        [HttpPost]
        [AllowAnonymous]
        public Task<bool> Create([FromForm] CreateUserDto createUserDto)
            => _identityServer.Post<bool, CreateUserDto>("api/User", createUserDto);

        [HttpPost]
        [Route("ChangePassword")]
        public Task<bool> ChangePassword([FromForm] ChangePasswordDto createUserDto)
            => _identityServer.Post<bool, ChangePasswordDto>("api/User/ChangePassword", createUserDto);
    }
}
