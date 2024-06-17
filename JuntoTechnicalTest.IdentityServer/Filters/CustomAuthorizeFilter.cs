using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JuntoTechnicalTest.IdentityServer.Filters
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {                
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
