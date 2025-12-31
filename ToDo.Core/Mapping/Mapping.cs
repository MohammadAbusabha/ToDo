using Mapster;
using ToDo.Core.Entities;
using ToDo.Core.Resources;

namespace ToDo.Core.Mapping
{
    public class Mapping
    {
        public static void ApplyMapping()
        {
            TypeAdapterConfig.GlobalSettings.Default.Settings.PreserveReference = true;

            ApplyModelToResourceMapping();
            ApplyResourceToModelMapping();
        }
        public static void ApplyResourceToModelMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<DataResource, Data>()
                .Ignore(dest => dest.UserId)
                .Ignore(dest => dest.User);
        }
        public static void ApplyModelToResourceMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<Data, DataResource>();
        }
    }
}