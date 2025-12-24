using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Interfaces
{
    public interface IRoleLevelResolver
    {
        public IEnumerable<int> RoleLevels(IEnumerable<string> roleNames);
    }
}
