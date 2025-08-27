using Asp.Versioning;
using AT_API.App_Code;
using AT_API.Extensions;
using AT_API.Model_Action;
using AT_API.Models;
using AT_API.Services;
using AT_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AT_API.Controllers
{
    [Route("api/v{version:apiVersion}/Authors")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    //[Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;
        public AuthorsController(WorkshopAPI context, ICourseLibraryRepository repo, IMapper mapper)
        {
            _authorService = new AuthorService(context);
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Get author data by ID
        /// </summary>
        /// <returns> Get author data by ID</returns>
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IEnumerable<AuthorDto>> Index()
        {
            var authourFromRepo = await _authorService.GetAllAuthorsAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authourFromRepo);
        }
        [HttpGet]
        [Route("GetAuthors")]
        public ActionResult GetAuthors([FromQuery] string mainCategory, string? searchQuery)
        {
            var data = _repo.GetAuthors(mainCategory, searchQuery);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(data));
        }
        [HttpGet("id")]
        public async Task<IActionResult> Read(Guid id)
        {
            return Ok(await _authorService.ReadByID(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto req)
        {
            if (ModelState.IsValid)
            {
                var res = await _authorService.CreateAuthorAsync(req);
                return CreatedAtAction(nameof(Create), new { id = res.Id }, res);
            }
            return BadRequest(ModelState);


        }
        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid? authorId)
        {
            var authourFromRepo = _repo.GetAuthor(authorId);
            if (authourFromRepo == null)
                return NotFound();
            return Ok(_mapper.Map<AuthorDto>(authourFromRepo));
        }
        [HttpPost]
        [Route("CreateAuthor")]
        public ActionResult<AuthorDto> CreateAuthor(Author_req author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _repo.AddAuthor(authorEntity);
            _repo.Save();
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtAction("GetAuthor", new { authorId = authorToReturn.Id },
            authorToReturn);
        }
        [HttpPut("{authorId}")]
        public ActionResult UpdateAuthor(Guid authorId, Author_up author)
        {
            var authorFromRepo = _repo.GetAuthor(authorId);
            if (authorFromRepo == null)
                return NotFound();
            _mapper.Map(author, authorFromRepo);

            _repo.UpdateAuthor(authorFromRepo);
            _repo.Save();
            return NoContent();
        }
        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return BadRequest();
            }
            var data = _repo.GetCourse(authorId);
            if (data.Count() > 0)
            {
                return BadRequest("Course > 0");
            }
            var author = _repo.GetAuthor(authorId);
            _repo.DeleteAuthor(author);
            _repo.Save();
            return NoContent();
        }
    }
}
