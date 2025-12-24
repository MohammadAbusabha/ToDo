using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDo.Api.Authorization;
using ToDo.Core.Interfaces;

namespace ToDo.Api.Handler
{
    public class PrivilegeAuthorizationHandler : AuthorizationHandler<PrivilegeRequirement>
    {
        private readonly IRoleLevelResolver _roleLevel;
        private readonly IPrivilegeEvaluator _privilegeEvaluator;

        public PrivilegeAuthorizationHandler(IRoleLevelResolver roleLevel, IPrivilegeEvaluator privilegeEvaluator)
        {
            _roleLevel = roleLevel;
            _privilegeEvaluator = privilegeEvaluator;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrivilegeRequirement requirement)
        {
            var rolenames = new List<string>();
            foreach (var role in context.User.FindAll(ClaimsIdentity.DefaultRoleClaimType))
            {
                rolenames.Add(role.Value);
            }

            var roleLevel = _roleLevel.RoleLevels(rolenames);
            if (_privilegeEvaluator.HasPrivilege(roleLevel, requirement.RequiredPrivilege))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}