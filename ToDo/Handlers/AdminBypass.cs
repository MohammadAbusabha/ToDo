using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ToDo.Handlers
{
    public class AdminBypass : AuthorizationHandler<IAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
