using ToDo.Core.Enums;
using ToDo.Core.Interfaces;

namespace ToDo.Core.Evaluator
{
    public class PrivilegeEvaluator : IPrivilegeEvaluator
    {
        public bool HasPrivilege(IEnumerable<int> role, Privileges privilege)
        {
            var maxLevel = role.Max();
            return maxLevel >= (int)privilege;
        }
    }
}