using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Entities
{
    public class Organization
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int ProjectId { get; set; }
        public List<Project> Project { get; set; }
    }
}
