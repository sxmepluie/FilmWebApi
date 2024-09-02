using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<FilmDtoForUpdate, Film>().ReverseMap();
            CreateMap<Film, FilmDto>();
            CreateMap<FilmDtoForInsertion, Film>();
            CreateMap<UserDtoForInsertion, User>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDtoForInsertion, Review>();
            CreateMap<UserDtoForInsertion, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();  
            CreateMap<User, UserDtoForRegistration>().ReverseMap();
            CreateMap<User, UserDtoForUpdate>().ReverseMap();

        }
    }
}
