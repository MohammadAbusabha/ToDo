namespace ToDo.Core.Entities
{
    public class Privilege
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; } = 0;
        public List<RolePrivilege> RolePrivilege { get; set; }
    }
}
