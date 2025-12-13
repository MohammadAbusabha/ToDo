using AutoMapper;
using Mapster;
using ToDo.Entitys;
using ToDo.Resources;

namespace ToDo
{
    public class Mapping
    {
        public static void ApplyMapping()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true).IgnoreNullValues(true);

            ApplyModelToResourceMapping();
            ApplyResourceToModelMapping();
        }
        public static void ApplyResourceToModelMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<DataResource, Data>().Map(dest => dest.Description, src => src.Description).Map(dest => dest.Name, src => src.Name);
        }
        public static void ApplyModelToResourceMapping()
        {
            TypeAdapterConfig.GlobalSettings.ForType<Data, DataResource>().Map(dest => dest.Description, src => src.Description).Map(dest => dest.Name, src => src.Name);
        }
    }
}