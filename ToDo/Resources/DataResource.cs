using System.ComponentModel.DataAnnotations;

namespace ToDo.Resources
{
    public class DataResource
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
    }

   public class SearchResource : DataResource
    {
        public bool MatchAny {  get; set; }
    }

    public class UpdateDataResource : DataResource
    {
        public int Id { get; set; }
    }
}
