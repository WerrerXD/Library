using Library_API.Application.Services;
using Library_API.Core.Contracts;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorsService authorsService, IMapper mapper)
        {
            _authorsService = authorsService;
            _mapper = mapper;
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<List<AuthorsResponse>>> GetAuthors()
        {
            var authors = await _authorsService.GetAllAuthors();

            var response = authors.Select(a => _mapper.Map<AuthorsResponse>(a));


            return Ok(response);
        }

        [HttpGet("GetAuthorByID")]
        public async Task<ActionResult<AuthorsResponse>> GetAuthorByID(Guid id)
        {
            var author = await _authorsService.GetAuthorById(id);
            if (author == null)
            {
                 return NotFound("Wrong Author ID");
            }

            var response = _mapper.Map<AuthorsResponse>(author);

            return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<Guid>> CreateAuthor([FromBody] AuthorsRequest request)
        {
            var author = Author.Create(
                Guid.NewGuid(),
                request.UserName,
                request.LastName,
                request.DateOfBirth,
                request.Country
                );

            var authorId = await _authorsService.CreateAuthor(author);

            return Ok(authorId);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<Guid>> UpdateAuthors(Guid id, [FromBody] AuthorsRequest request)
        {
            var authorId = await _authorsService.UpdateAuthor(id,
                request.UserName,
                request.LastName,
                request.DateOfBirth,
                request.Country
                );

            return Ok(authorId);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult<Guid>> DeleteAuthors(Guid id)
        {
            return Ok(await _authorsService.DeleteAuthor(id));
        }


        [HttpGet("GetAuthorsBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetAuthorsBooks(string lastName)
        {
            var books = await _authorsService.GetAuthorsBooks(lastName);

            if(books == null)
            {
                return NotFound("Wrong Authors LastName");
            }

            var response = books.Select(b => _mapper.Map<BooksResponse>(b));

            return Ok(response);
        }
    }
}
