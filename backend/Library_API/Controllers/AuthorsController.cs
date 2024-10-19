using Library_API.Application.Contracts;
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

                var response = _mapper.Map<AuthorsResponse>(author);

                return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<Guid>> CreateAuthor([FromBody] AuthorsRequest request)
        {
            
                var author = _mapper.Map<Author>(request);
                var authorId = await _createAuthorUseCase.ExecuteAsync(author);
                return Ok(authorId);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult> UpdateAuthors(Guid id, [FromBody] AuthorsRequest request)
        {
            
                var author = _mapper.Map<Author>(request);
                author.Id = id;
                await _updateAuthorUseCase.ExecuteAsync(author);
                return Ok();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult> DeleteAuthors(Guid id)
        {
                await _deleteAuthorUseCase.ExecuteAsync(id);
                return Ok();
        }


        [HttpGet("GetAuthorsBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetAuthorsBooks(string name, string lastName)
        {
                var books = await _getAuthorsBooksUseCase.ExecuteAsync(name, lastName);

                var response = books.Select(b => _mapper.Map<BooksResponse>(b));

                return Ok(response);
        }
    }
}
