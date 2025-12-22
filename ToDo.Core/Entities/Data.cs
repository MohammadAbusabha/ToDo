namespace ToDo.Core.Entities
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}