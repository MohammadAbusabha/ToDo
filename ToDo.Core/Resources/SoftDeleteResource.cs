using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Resources
{
    public class SoftDeleteResource
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = true;
    }
}
