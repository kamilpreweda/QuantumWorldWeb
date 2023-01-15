using AutoMapper;
using QuantumWorld.Core.Domain;
using QuantumWorld.Infrastructure.DTO;

namespace QuantumWorld.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            })
            .CreateMapper();
    }
}
