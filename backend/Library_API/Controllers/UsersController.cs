using Library_API.Application.Services;
using Library_API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Library_API.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Library_API.Core.Abstractions;
using AutoMapper;
using System.Web.Helpers;

namespace Library_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task <ActionResult> Register([FromBody]RegisterUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
                return BadRequest("User data can not be empty");

            await _userService.Register(request.Username, request.Email, request.Password);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("User data can not be empty");

            var token = await _userService.Login(request.Email, request.Password);

            if (token == null)
            {
                return NotFound("Failed to login");
            }

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

            if (books == null)
            {
                return NotFound("No books were taken");
            }

            var response = books.Select(b => _mapper.Map<BooksResponse>(b));

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
        public async Task<ActionResult> AddBookByTitleAndAuthor(string title, string authorName)
        {
            var email = HttpContext.Request.Cookies["UserEmail"];

            await _userService.AddBookByTitleAndAuthor(title, authorName, email);

            return Ok()
                ?? throw new Exception("No books with this title and author");
        }



    }
}
