using AutoMapper;
using Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces;
using Library_API.Core.Contracts;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;

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
        private readonly IAddCoverToBookUseCase _addCoverToBookUseCase;

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
                               IAddCoverToBookUseCase addCoverToBookUseCase,
                               IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByIsbnUseCase = getBookByIsbnUseCase;
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _addCoverToBookUseCase = addCoverToBookUseCase;
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
            var book = await _getBookByIdUseCase.ExecuteAsync(id);

            if (book == null) 
            {
                return NotFound("Wrong Book ID");
            }

            var response = _mapper.Map<BooksResponse>(book);

            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetBookByIspn")]
        public async Task<ActionResult<BooksResponse>> GetBookByISBN(int isbn)
        {
            var book = await _getBookByIsbnUseCase.ExecuteAsync(isbn);


            if (book == null)
            {
                return NotFound("Wrong Book ISBN");
            }

            var response = _mapper.Map<BooksResponse>(book);

            return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("AddCoverToBook")]
        public async Task<ActionResult<string>> AddCoverToBook(Guid id, IFormFile coverPhoto)
        {
            if (coverPhoto == null)
            {
                return BadRequest("Image can not be empty");
            }

            string folder = "bookscovers/";
            folder += Guid.NewGuid().ToString() + "_" + coverPhoto.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            var url = await _addCoverToBookUseCase.ExecuteAsync(id, coverPhoto, serverFolder, folder);

            if (url == null)
            {
                return NotFound("Wrong Book ID");
            }

            return Ok(url);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateBook")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var book = Book.Create(
                Guid.NewGuid(),
                request.ISBN,
                request.Title,
                request.Genre,
                request.Description,
                request.AuthorName,
                request.Datein,
                request.Dateout,
                request.AuthorId,
                ""
                );

            var bookId = await _createBookUseCase.ExecuteAsync(book);

            return Ok(bookId);
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BooksRequest request)
        {
            var bookId = await _updateBookUseCase.ExecuteAsync(id,
                request.ISBN,
                request.Title,
                request.Genre,  
                request.Description,
                request.AuthorName,
                request.Datein,
                request.Dateout,
                request.AuthorId
                );

            return Ok(bookId);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult<Guid>> DeleteBooks(Guid id)
        {
            return Ok(await _deleteBookUseCase.ExecuteAsync(id));
        }
    }
}
