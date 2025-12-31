using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Core.Aggregate
{
    public class DataAggregate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid OwnerId { get; private set; }

        public DataAggregate(Guid ownerId, string name, string desc)
        {
            Id = new Guid();
            Name = name;
            Description = desc;
            OwnerId = ownerId;
        }
        private bool IsOwner(Guid ownerId)
        {
            return (ownerId == OwnerId);
        }

        public void Set(Guid ownerId, string name, string desc, bool isAdmin)
        {
            if (IsOwner(ownerId) || isAdmin)
            {
                Name = name;
                Description = desc;
            }
        }
    }
}
