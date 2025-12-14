using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Enums;

namespace ToDo.Handlers
{
    public class AdminBypass : AuthorizationHandler<IAuthorizationRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole("Admin") | context.User.HasClaim("Permission", "Owner"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
