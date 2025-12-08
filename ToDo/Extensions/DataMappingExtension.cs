using ToDo.Dto;
using ToDo.Models;

namespace ToDo.Extensions
{
    public static class DataMappingExtension
    {
        public static DataDTO ToDto(this Data data)
        {
            return new DataDTO
            {
                Name = data.Name,
                Description = data.Description,
            };
        }
        public static Data ToEntity(this DataDTO datadto)
        {
            return new Data
            {
                Name = datadto.Name,
                Description = datadto.Description,
            };
        }
    }
}
