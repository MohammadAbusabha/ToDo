using ToDo.Core.Entities;
using ToDo.Core.SpecTest;

namespace ToDo.Core.Interfaces
{
    public interface IOrganizationRepository
    {
        public Task SoftDeleteAsync(ISpecification<Organization> specification);
    }
}
