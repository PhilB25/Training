using AT_API.Extensions;
using AT_API.Model_Action;
using AT_API.Models;
using AutoMapper;

namespace AT_API.App_Code
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>().ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(
            dest => dest.Age,
            opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
            //--------------------------------------------------------
            CreateMap<Author_req, Author>().ForMember(
                r=> r.Id,o=>o.MapFrom(src=> Guid.NewGuid()));
            CreateMap<Author_up, Author>();
        }
    }
}
