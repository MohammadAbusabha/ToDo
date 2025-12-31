using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Entities
{
    public class PrivilegeRole
    {
        public int Id { get; set; }
        public Guid RolesId {  get; set; }
        public List<ApplicationRole> Roles { get; set; }
        public Guid PrivilegesId { get; set;  }
        public List<Privilege> Privileges { get; set; }
    }
}
