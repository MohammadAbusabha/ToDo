using Microsoft.AspNetCore.Identity;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.ServiceTest;

namespace ToDo.Infrastructure.SpecTest
{
    public class PrivilegeToRoleLink : IPrivilegeRoleLink
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly DataContext _context;
        public PrivilegeToRoleLink(RoleManager<ApplicationRole> roleManager, DataContext context)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task CreateLinkAsync(string role, List<string> privileges)// works but needs optimization 
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                var roleId = await _roleManager.GetRoleIdAsync(await _roleManager.FindByNameAsync(role));
                foreach (var priv in privileges)
                {
                    //var privilege = await _context.PrivilegeTable.AnyAsync(x => x.Name == priv);
                    var privilege = _context.PrivilegeTable.Where(x => x.Name == priv).Select(x => x.Id);
                    if (privilege != null)
                    {
                        var privId = await _context.PrivilegeTable.Where(x=>x.Name == priv).Select(x=>x.Id).FirstAsync();
                        await _context.PrivilegeRoles.AddAsync(new PrivilegeRole() { PrivilegesId = privId, RolesId = Guid.Parse(roleId) });
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
