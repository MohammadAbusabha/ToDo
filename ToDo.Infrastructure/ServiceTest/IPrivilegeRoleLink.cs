using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Infrastructure.ServiceTest
{
    public interface IPrivilegeRoleLink
    {
        public Task CreateLinkAsync(string role, List<string> privilege);
    }
}
