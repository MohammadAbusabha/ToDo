using ToDo.Core.Entities;

namespace ToDo.Infrastructure.BaseSpecifications
{
    public class GetDataById : BaseSpecifications<Data>
    {
        //public GetDataById(int id, ICurrentUserService user) : base(t=>t.Id == id, t=>t.UserId == user.CurrentUserId())
        public GetDataById(int id) : base(t => t.Id == id)
        {
        }
    }
}
