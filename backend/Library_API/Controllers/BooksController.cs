using Library_API.Application.Services;
using Library_API.Contracts;
using Library_API.Core.Models;
using Library_API.DataAccess.Entities;
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

        public BooksController(IBooksService booksService, IWebHostEnvironment webHostEnvironment)
        {
            _booksService = booksService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getAllBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.ISBN, b.Title, b.Genre, b.Description, b.AuthorName, b.DateIn, b.DateOut, b.AuthorId));

            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetBookById")]
        public async Task<ActionResult<BooksResponse>> GetBookByID(Guid id)
        {
            var book = await _booksService.GetBookById(id);

            var response = book;

            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetBookByIspn")]
        public async Task<ActionResult<BooksResponse>> GetBookByISBN(int isbn)
        {
            var book = await _booksService.GetBookByISBN(isbn);

            var response = book;

            return Ok(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateBookWithCover")]
        public async Task<ActionResult<Guid>> CreateBookWithCover([FromForm]BooksRequestWithPicture request)
        {
            var book = new Book(
                Guid.NewGuid(),
                request.ISBN,
                request.Title,
                request.Genre,
                request.Description,
                request.AuthorName,
                request.Datein,
                request.Dateout,
                request.AuthorId,
                request.CoverPhoto,
                ""
                );

            if (book.CoverPhoto != null)
            {
                string folder = "bookscovers/";
                folder += Guid.NewGuid().ToString() + "_" + book.CoverPhoto.FileName;

                book.CoverImageUrl = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await book.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

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
