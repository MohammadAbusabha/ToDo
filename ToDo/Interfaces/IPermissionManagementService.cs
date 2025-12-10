using System.Threading.Tasks;
using ToDo.Resources;

namespace ToDo.Interfaces
{
    public interface IPermissionManagementService
    {
        public Task Addpermission(PermissionResource permissionResource);
    }
}
