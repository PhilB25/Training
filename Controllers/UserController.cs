using Asp.Versioning;
using AT_API.App_Code;
using AT_API.Models;
using AT_API.Services;
using AT_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AT_API.Controllers
{
    [Route("api/v{version:apiVersion}/Authors")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;
        public UserController(WorkshopAPI context, ICourseLibraryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult Authenticate([FromBody] User user)
        {
            var userForReturn = _repo.Authenticate(user.Username, user.Password);
            if (userForReturn == null)
            {
                return BadRequest(new { message = "Username or Password incorrect" });
            }
            return Ok(userForReturn);
        }
        [HttpPost("Register")]
        public ActionResult Register([FromBody] User user)
        {
            if (!_repo.IsUniqueUser(user.Username))
            {
                return BadRequest(new { message = "Username is exists" });
            }
            var userForReturn = _repo.Register(user.Username, user.Password);
            if (userForReturn == null)
            {
                return BadRequest(new { message = "Register Error" });
            }
            return Ok(userForReturn);
        }
    }
}
