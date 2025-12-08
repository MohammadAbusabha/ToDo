using System.ComponentModel.DataAnnotations;

namespace ToDo.Dto
{
    public class DataDTO
    {
        public string? Name {  get; set; }
        public string? Description { get; set; }
    }

   public class SearchDTO : DataDTO
    {
        public bool MatchAny {  get; set; }
    }

    public class UpdateDataDTO : DataDTO
    {
        public int Id { get; set; }
    }
}
