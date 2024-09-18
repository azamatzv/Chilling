using AutoMapper;
using Entities.DTO.User;
using Entities.Model.User;

namespace WebAPI.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
    }
}