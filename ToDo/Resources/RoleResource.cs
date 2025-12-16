namespace ToDo.Resources
{
    public class RoleResource : RoleUser
    {
        public string RoleName { get; set; }
    }
    public class RoleUser
    {
        public string UserName { get; set; }
    }
    public class RoleValue : RoleUser
    {
        public int Value { get; set; } = 1;
    }
}
