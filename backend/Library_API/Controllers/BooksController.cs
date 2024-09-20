using AutoMapper;
using Library_API.Application.Services;
using Library_API.Core.Contracts;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBooksService _booksService;
        public static IWebHostEnvironment? _webHostEnvironment;
        private readonly IMapper _mapper;

        public BooksController(IBooksService booksService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _booksService = booksService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpGet("getAllBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => _mapper.Map<BooksResponse>(b));

            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetBookById")]
        public async Task<ActionResult<BooksResponse>> GetBookByID(Guid id)
        {
            var book = await _booksService.GetBookById(id);

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
            var book = await _booksService.GetBookByISBN(isbn);


            if (book == null)
            {
                return NotFound("Wrong Book ISBN");
            }

            var response = _mapper.Map<BooksResponse>(book);

            return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("AddCoverToBook")]
        public async Task<ActionResult<string>> AddCoverToBook(Guid id, IFormFile CoverPhoto)
        {
            var book = await _booksService.GetBookById(id);

            if (book == null)
            {
                return NotFound("Wrong Book ID");
            }

            if (CoverPhoto != null)
            {
                string folder = "bookscovers/";
                folder += Guid.NewGuid().ToString() + "_" + CoverPhoto.FileName;

                book.CoverImageUrl = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            return Ok(book.CoverImageUrl);
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

            var bookId = await _booksService.CreateBook2(book, book.AuthorId);

            return Ok(bookId);
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult<Guid>> UpdateBooks(Guid id, [FromBody] BooksRequest request)
        {
            var bookId = await _booksService.UpdateBook(id,
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
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}
