namespace ToDo.Core.Entities
{
    public class Privilege
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PrivilegeRole> PrivilegeRole { get; set; }
    }
}
