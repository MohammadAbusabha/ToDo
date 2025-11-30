using ToDo.Dto;
using ToDo.Models;

namespace ToDo.Extensions
{
    public static class DataMappingExtension
    {
        public static DataDto ToDto(this Data data)
        {
            return new DataDto
            {
                Name = data.Name,
                Description = data.Description,
            };
        }
        public static Data ToEntity(this DataDto datadto)
        {
            return new Data
            {
                Name = datadto.Name,
                Description = datadto.Description,
            };
        }
    }
}
