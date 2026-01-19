using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Entities
{
    public class Device
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
