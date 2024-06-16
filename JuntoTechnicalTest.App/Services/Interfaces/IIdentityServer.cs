using IdentityModel.Client;
using JuntoTechnicalTest.Common.Dto;

namespace JuntoTechnicalTest.App.Services.Interfaces
{
    public interface IIdentityServer: IClientServer
    {
        Task<TokenResponse?> GetToken(LoginDto login);
    }
}
