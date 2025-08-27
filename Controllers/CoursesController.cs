using AT_API.Model_Action;
using AT_API.Models;
using AT_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace AT_API.Controllers
{
    [ApiController]
    [Route("api/author/{authorId}/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;
        public CoursesController(ICourseLibraryRepository repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCourses(Guid authorId)
        {
            if (!_repo.AuthorExists(authorId))
                return NotFound();
            var coursesForAuthorFromRepo = _repo.GetCourse(authorId);
            return Ok(_mapper.Map<IEnumerable<Course_res>>(coursesForAuthorFromRepo));
        }
        [HttpGet("{courseId}")]
        public IActionResult GetCourseForAuthor(Guid authorId, int courseId)
        {
            if (!_repo.AuthorExists(authorId))
                return NotFound();
            var courseForAuthorFromRepo = _repo.GetCourseById(authorId, courseId);
            if (courseForAuthorFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<Course_res>(courseForAuthorFromRepo));
        }
        [HttpPost]
        [Route("CreateCourse")]
        public ActionResult<AuthorDto> CreateCourse(Course_req course)
        {
            var authorEntity = _mapper.Map<Course>(course);
            _repo.AddCourse(authorEntity);
            _repo.Save();
            var authorToReturn = _mapper.Map<Course_res>(authorEntity);
            return CreatedAtAction("CreateCourse", new { authorId = authorToReturn.Id },
            authorToReturn);
        }
        [HttpPut("{courseId}")]
        public IActionResult UpdateCourse(int courseId, Course_up course)
        {
            if (_repo.GetCourse(courseId) == null)
                return NotFound();
            var courseForAuthorFromRepo = _repo.GetCourseById(courseId);
            if (courseForAuthorFromRepo == null)
                return NotFound();
            _mapper.Map(course, courseForAuthorFromRepo);
            _repo.UpdateCourse(courseForAuthorFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdateCourse(int courseId, JsonPatchDocument<Course>
patchDocument)
        {
            //if (!_repo.AuthorExists(authorId))
            //{
            //    return BadRequest();
            //}
            var course = _repo.GetCourseById(courseId);
            if (course == null)
            {
                return BadRequest();
            }
            patchDocument.ApplyTo(course, ModelState);
            if (!TryValidateModel(course))
            {
                return ValidationProblem(ModelState);
            }
            _repo.UpdateCourse(course);
            _repo.Save();
            return NoContent();

        }
        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(Guid authorId, int courseId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return BadRequest();
            }
            var course = _repo.GetCourseById(authorId, courseId);
            if (course == null)
            {
                return BadRequest();

            }
            _repo.DeleteCourse(course);
            _repo.Save();
            return NoContent();
        }
    }
}
