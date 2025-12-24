using Microsoft.AspNetCore.Authorization;
using ToDo.Core.Enums;

namespace ToDo.Api.Authorization
{
    public class PrivilegeRequirement : IAuthorizationRequirement
    {
        public Privileges RequiredPrivilege { get; }
        public PrivilegeRequirement(Privileges privileges)
        {
            RequiredPrivilege = privileges;
        }
    }
}