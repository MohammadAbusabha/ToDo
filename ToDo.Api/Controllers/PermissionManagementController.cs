using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Resources;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPrivilegeManagementService _permissionManagementService;
        public PermissionManagementController(IPrivilegeManagementService permissionManagementService)// Controller useless for now
        {
            _permissionManagementService = permissionManagementService;
        }

        [HttpPost]
        public async Task AddPermissions(PrivilegeResource privilegeResource)
        {
            //await _permissionManagementService.Addpermission(permissionResource);
        }
    }
}
