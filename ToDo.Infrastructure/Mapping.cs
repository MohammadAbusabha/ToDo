using Mapster;
using ToDo.Core.Entities;
using ToDo.Core.Resources;

namespace ToDo.Infrastructure
{
    public class Mapping
    {
        //propeply useless

        //private readonly ICurrentUserService _user;
        //public Mapping(ICurrentUserService currentUserService)
        //{
        //    _user = currentUserService;
        //}
        public static void ApplyMapping()
        {
            TypeAdapterConfig.GlobalSettings.Default.Settings.PreserveReference = true;

            ApplyModelToResourceMapping();
            ApplyResourceToModelMapping();
        }
        public static void ApplyResourceToModelMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<DataResource, Data>()
                .Ignore(dest => dest.UserId);
        }
        public static void ApplyModelToResourceMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<Data, DataResource>();
        }
    }
}