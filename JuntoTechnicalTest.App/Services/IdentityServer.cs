﻿
using IdentityModel.Client;
using JuntoTechnicalTest.App.Services.Interfaces;
using JuntoTechnicalTest.Common.Dto;

namespace JuntoTechnicalTest.App.Services
{
    public class IdentityServer : ClientServerBase, IIdentityServer
    {
        private readonly IConfiguration _configuration;
        protected IdentityServer(HttpClient httpClient, 
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) : base(httpClient, httpContextAccessor)
        {
            _configuration = configuration;
        }
        public async Task<TokenResponse?> GetToken(LoginDto login)
        {

            var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{_httpClient?.BaseAddress?.OriginalString}/connect/token",

                ClientId = _configuration["IdentityServer:Client"] ?? "",
                ClientSecret = _configuration["IdentityServer:ClientSecret"],
                Scope = _configuration["IdentityServer:Scope"],

                UserName = login.Email,
                Password = login.Password
            });
            if (response.IsError)
            {
               // throw new ValidationException(new Dictionary<string, string[]> { { "token", new string[] { response.ErrorDescription ?? "" } } });
            }
            return response;
        }
    }
}
