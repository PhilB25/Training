using AT_API.Extensions;
using AT_API.Model_Action;
using AT_API.Models;
using AutoMapper;

namespace AT_API.App_Code
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, Course_res>();
            CreateMap<Course_req, Course>();
            CreateMap<Course_up, Course>();
        }
    }
}
