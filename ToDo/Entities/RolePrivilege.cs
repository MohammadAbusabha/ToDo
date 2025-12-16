using System;
using System.Collections.Generic;

namespace ToDo.Entities
{
    public class RolePrivilege
    {
        public Guid RoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public int PrivilegeId { get; set; }
        public Privilege privilege { get; set; }
    }
}
