using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Entitys
{
    public class Data
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid Userid { get; set; }
    }
}