using IdentityModel.Client;
using JuntoTechnicalTest.App.Services.Interfaces;
using JuntoTechnicalTest.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JuntoTechnicalTest.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IIdentityServer identityServer) : ControllerBase
    {
        private readonly IIdentityServer _identityServer = identityServer;
        [HttpPost]
        public Task<TokenResponse?> Login([FromForm] LoginDto login)
           => _identityServer.GetToken(login);

    }
}
