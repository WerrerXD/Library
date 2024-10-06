using AutoMapper;
using Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces;
using Library_API.Application.Contracts;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;
using Library_API.Application.Services;
using Library_API.Core.Abstractions;

namespace Library_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIdUseCase;
        private readonly IGetBookByIsbnUseCase _getBookByIsbnUseCase;
        private readonly ICreateBookUseCase _createBookUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;

        private readonly IBookCoverService _bookCoverService;

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(
                               IMapper mapper,
                               IGetAllBooksUseCase getAllBooksUseCase,
                               IGetBookByIdUseCase getBookByIdUseCase,
                               IGetBookByIsbnUseCase getBookByIsbnUseCase,
                               ICreateBookUseCase createBookUseCase,
                               IUpdateBookUseCase updateBookUseCase,
                               IDeleteBookUseCase deleteBookUseCase,
                               IBookCoverService bookCoverService,
                               IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByIsbnUseCase = getBookByIsbnUseCase;
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _bookCoverService = bookCoverService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getAllBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _getAllBooksUseCase.ExecuteAsync();

            var response = books.Select(b => _mapper.Map<BooksResponse>(b));

            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetBookById")]
        public async Task<ActionResult<BooksResponse>> GetBookByID(Guid id)
        {
            try
            {
                var book = await _getBookByIdUseCase.ExecuteAsync(id);

                var response = _mapper.Map<BooksResponse>(book);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetBookByIspn")]
        public async Task<ActionResult<BooksResponse>> GetBookByISBN(int isbn)
        {
            try
            {
                var book = await _getBookByIsbnUseCase.ExecuteAsync(isbn);

                var response = _mapper.Map<BooksResponse>(book);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("AddCoverToBook")]
        public async Task<ActionResult<string>> AddCoverToBook(Guid id, IFormFile coverPhoto)
        {
            try
            {
                var url = await _bookCoverService.AddCoverToBookAsync(id, coverPhoto);

                return Ok(url);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateBook")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            
            try
            {
                var book = _mapper.Map<Book>(request);
                var bookId = await _createBookUseCase.ExecuteAsync(book);

                return Ok(bookId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBooks(Guid id, [FromBody] BooksRequest request)
        {

            try
            {
                var book = _mapper.Map<Book>(request);
                book.Id = id;
                await _updateBookUseCase.ExecuteAsync(book);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBooks(Guid id)
        {
            try
            {
                await _deleteBookUseCase.ExecuteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
