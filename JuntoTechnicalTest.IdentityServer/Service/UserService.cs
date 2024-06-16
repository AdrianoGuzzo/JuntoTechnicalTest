using JuntoTechnicalTest.Common.Dto;
using JuntoTechnicalTest.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace JuntoTechnicalTest.IdentityServer.Service
{
    public class UserService(UserManager<ApplicationUser> userManager)
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<bool> CreateUser(CreateUserDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                //throw new ValidationException(new Dictionary<string, string[]> {
                //        {"identityServer", result.Errors.Select(x => x.Code).ToArray()}
                //    });
            }
            return result.Succeeded;
        }
    }
}
