using AutoMapper;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Models;

namespace ProjectMGN.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterUserRequest>().ReverseMap();
        }

        
    }
}
