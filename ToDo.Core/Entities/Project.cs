using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Entities
{
    public class Project
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int DeviceId { get; set; }
        public List<Device> Device{ get; set; }
    }
}
