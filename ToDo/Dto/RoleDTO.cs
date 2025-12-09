namespace ToDo.Dto
{
    public class RoleDTO : UserRoleDTO
    {
        public string RoleName { get; set; }
    }
    public class UserRoleDTO 
    {
        public string UserName { get; set; }
    }
}
