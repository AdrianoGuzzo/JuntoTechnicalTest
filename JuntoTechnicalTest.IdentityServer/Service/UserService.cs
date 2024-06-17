using JuntoTechnicalTest.Common.Dto;
using JuntoTechnicalTest.Common.Exceptions;
using JuntoTechnicalTest.IdentityServer.Data.Migrations;
using JuntoTechnicalTest.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JuntoTechnicalTest.IdentityServer.Service
{
    public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string CONTEXT = "user";

        public string? GetUserId()
        {
            var context = httpContextAccessor.HttpContext;

            var headers = context?.Request.Headers ?? throw new Exception("Not found context");

            // Exemplo: obter o valor de um header específico
            if (!headers.TryGetValue("user-id", out var userId))
            {
                // Faça algo com o valor do header Authorization
                throw new Exception("user-id not found");
            }

            return userId;
        }

        public async Task<bool> CreateUser(CreateUserDto createUserDto)
        {
            var user = new ApplicationUser
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException(new Dictionary<string, string[]> {
                        {CONTEXT, result.Errors.Select(x => x.Code).ToArray()}
                    });
            }
            return result.Succeeded;
        }
        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto.NewPassword != changePasswordDto.NewPasswordConfirmation)
                throw new ValidationException(new Dictionary<string, string[]> {
                        {CONTEXT, ["Password confirmation is different"] }
                    });
            var user = await _userManager.FindByIdAsync(GetUserId());

            if (user == null)
                throw new ValidationException(new Dictionary<string, string[]> {
                        {CONTEXT, ["User not found"] }
                    });

            bool isCheckPassword = await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);

            if (!isCheckPassword)
                throw new ValidationException(new Dictionary<string, string[]> {
                        {CONTEXT, ["Old password wrong"] }
                    });         


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new ValidationException(new Dictionary<string, string[]> {
                        {CONTEXT, result.Errors.Select(x => x.Code).ToArray()}
                    });
            }
        }
    }
}
