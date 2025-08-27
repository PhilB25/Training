using Asp.Versioning;
using AT_API.App_Code;
using AT_API.Extensions;
using AT_API.Model_Action;
using AT_API.Models;
using AT_API.Services;
using AT_API.Services.Interfaces;
using AutoMapper;
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
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    //[Route("api/[controller]")]
    public class Authors2Controller : Controller
    {
        private readonly AuthorService _authorService;
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;
        public Authors2Controller(WorkshopAPI context, ICourseLibraryRepository repo, IMapper mapper)
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
        public async Task<ActionResult> Index()
        {
            var authourFromRepo = await _authorService.GetAllAuthorsAsync();
            //return _mapper.Map<IEnumerable<AuthorDto>>(authourFromRepo);
            return Ok("ggbb");
        }
        
    }
}
