namespace ToDo.Core.Resources
{
    public class CreateDataResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DataResource : CreateDataResource
    {
        public int Id { get; set; }
    }
}
