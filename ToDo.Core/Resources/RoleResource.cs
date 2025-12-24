namespace ToDo.Core.Resources
{
    public class RoleResource : RoleUser
    {
        public string RoleName { get; set; }
    }
    public class RoleUser
    {
        public string UserName { get; set; }
    }
}
