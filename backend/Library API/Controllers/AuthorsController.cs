using Library_API.Application.Services;
using Library_API.Contracts;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<List<AuthorsResponse>>> GetAuthors()
        {
            var authors = await _authorsService.GetAllAuthors();

            var response = authors.Select(a => new AuthorsResponse(a.Id, a.UserName, a.LastName, a.DateOfBirth, a.Country));

            return Ok(response);
        }

        [HttpGet("GetAuthorByID")]
        public async Task<ActionResult<AuthorsResponse>> GetAuthorByID(Guid id)
        {
            var author = await _authorsService.GetAuthorById(id);

            var response = author;

            return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<Guid>> CreateAuthor([FromBody] AuthorsRequest request)
        {
            var (author, error) = Author.Create(
                Guid.NewGuid(),
                request.UserName,
                request.LastName,
                request.DateOfBirth,
                request.Country
                );

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            var authorId = await _authorsService.CreateAuthor(author.UserName, author.LastName, author.DateOfBirth, author.Country);

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
           
            var response = books.Select(b => new BooksResponse(b.Id, b.ISBN, b.Title, b.Genre, b.Description, b.AuthorName, b.DateIn, b.DateOut, b.AuthorId));

            return Ok(response);
        }
    }
}
