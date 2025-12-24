using System.Security.Claims;
using ToDo.Core.Enums;

namespace ToDo.Core.Interfaces
{
    public interface IPrivilegeEvaluator
    {
        public bool HasPrivilege(IEnumerable<int> role, Privileges privilege);
    }
}
