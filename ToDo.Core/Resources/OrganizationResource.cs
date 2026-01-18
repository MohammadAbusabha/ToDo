using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Resources
{
    public class OrganizationResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateOrgResource : OrganizationResource
    {
        public int Id { get; set; }
    }
}
