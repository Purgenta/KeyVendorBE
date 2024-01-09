using AutoMapper;
using KeyVendor.Application.Common.Dto;

namespace KeyVendor.Application.Mappers.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, Domain.Entities.User>().ReverseMap();
    }
}