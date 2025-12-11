using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Resources;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPermissionManagementService _permissionManagementService;
        public PermissionManagementController(IPermissionManagementService permissionManagementService)
        {
            _permissionManagementService = permissionManagementService;
        }

        [HttpPost]
        public async Task AddPermissions(PermissionResource permissionResource)
        {
            await _permissionManagementService.Addpermission(permissionResource);
        }
    }
}
