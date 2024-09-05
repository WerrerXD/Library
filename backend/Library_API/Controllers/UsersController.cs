using Library_API.Application.Services;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Library_API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Library_API.Core.Abstractions;

namespace Library_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task <ActionResult> Register([FromBody]RegisterUserRequest request)
        {
            await _userService.Register(request.Username, request.Email, request.Password);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginUserRequest request)
        {
            var token = await _userService.Login(request.Email, request.Password);


            HttpContext.Response.Cookies.Append("tasty-cookies", token);

            if (request.Email == "adminmail")
                HttpContext.Response.Cookies.Append("IsAdmin", "Yes");
            else
                HttpContext.Response.Cookies.Append("IsAdmin", "No");

            HttpContext.Response.Cookies.Append("UserEmail", request.Email);

            return Ok();
        }

        [Authorize]
        [HttpPost("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                HttpContext.Response.Cookies.Delete(cookie);

            }

            return Ok();
        }

        [Authorize]
        [HttpGet("GetUserBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetUserBooks()
        {
            var email = HttpContext.Request.Cookies["UserEmail"];

            var books = await _userService.GetUserBooks(email);


            var response = books.Select(b => new BooksResponse(b.Id, b.ISBN, b.Title, b.Genre, b.Description, b.AuthorName, b.DateIn, b.DateOut, b.AuthorId));

            return Ok(response);
        }

        [Authorize]
        [HttpPost("AddBookByISBN")]
        public async Task<ActionResult> AddBookByISBN(int isbn)
        {
            var email = HttpContext.Request.Cookies["UserEmail"];

            await _userService.AddBookByISBN(isbn, email);

            return Ok();
        }

        [Authorize]
        [HttpPost("AddBookByTitleAndAuthor")]
        public async Task<ActionResult> AddBookByTitleAndAuthor(string title = "", string authorName = "")
        {
            var email = HttpContext.Request.Cookies["UserEmail"];

            await _userService.AddBookByTitleAndAuthor(title, authorName, email);

            return Ok();
        }



    }
}
