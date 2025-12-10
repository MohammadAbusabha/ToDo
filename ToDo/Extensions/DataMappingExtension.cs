using ToDo.Resources;
using ToDo.Entitys;

namespace ToDo.Extensions
{
    public static class DataMappingExtension
    {
        public static DataResource ToDto(this Data data)
        {
            return new DataResource
            {
                Name = data.Name,
                Description = data.Description,
            };
        }
        public static Data ToEntity(this DataResource datadto)
        {
            return new Data
            {
                Name = datadto.Name,
                Description = datadto.Description,
            };
        }
    }
}
