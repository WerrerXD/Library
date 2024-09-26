using Library_API.Core.Contracts;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces;

namespace Library_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {

        private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;
        private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly IGetAuthorsBooksUseCase _getAuthorsBooksUseCase;

        private readonly IMapper _mapper;

        public AuthorsController(
                                 IMapper mapper,
                                 IGetAllAuthorsUseCase getAllAuthorsUseCase,
                                 IGetAuthorByIdUseCase getAuthorByIdUseCase,
                                 ICreateAuthorUseCase createAuthorUseCase,
                                 IUpdateAuthorUseCase updateAuthorUseCase,
                                 IDeleteAuthorUseCase deleteAuthorUseCase,
                                 IGetAuthorsBooksUseCase getAuthorsBooksUseCase)
        {
            _mapper = mapper;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _createAuthorUseCase = createAuthorUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorsBooksUseCase = getAuthorsBooksUseCase;
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<List<AuthorsResponse>>> GetAuthors()
        {
            var authors = await _getAllAuthorsUseCase.ExecuteAsync();

            var response = authors.Select(a => _mapper.Map<AuthorsResponse>(a)).ToList();

            return Ok(response);
        }

        [HttpGet("GetAuthorByID")]
        public async Task<ActionResult<AuthorsResponse>> GetAuthorByID(Guid id)
        {
            var author = await _getAuthorByIdUseCase.ExecuteAsync(id);
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

            var authorId = await _createAuthorUseCase.ExecuteAsync(author);

            return Ok(authorId);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<Guid>> UpdateAuthors(Guid id, [FromBody] AuthorsRequest request)
        {
            var authorId = await _updateAuthorUseCase.ExecuteAsync(id,
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
            return Ok(await _deleteAuthorUseCase.ExecuteAsync(id));
        }


        [HttpGet("GetAuthorsBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetAuthorsBooks(string lastName)
        {
            var books = await _getAuthorsBooksUseCase.ExecuteAsync(lastName);

            if(books == null)
            {
                return NotFound("Wrong Authors LastName");
            }

            var response = books.Select(b => _mapper.Map<BooksResponse>(b));

            return Ok(response);
        }
    }
}
