using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ToDo.Infrastructure.Helpers
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
