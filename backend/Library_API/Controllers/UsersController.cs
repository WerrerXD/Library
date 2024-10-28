using Library_API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Library_API.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Library_API.Core.Abstractions;
using AutoMapper;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;

namespace Library_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IAddBookToUserByIsbnUseCase _addBookToUserByIsbnUseCase;
        private readonly IAddBookToUserByTitleAuthorUseCase _addBookToUserByTitleAuthorUseCase;
        private readonly IGetUserBooksUseCase _getUserBooksUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;

        private readonly IMapper _mapper;
        public UsersController(
                               IMapper mapper,
                               IAddBookToUserByIsbnUseCase addBookToUserByIsbnUseCase,
                               IAddBookToUserByTitleAuthorUseCase addBookToUserByTitleAuthorUseCase,
                               IGetUserBooksUseCase getUserBooksUseCase,
                               ILoginUserUseCase loginUserUseCase,
                               IRegisterUserUseCase registerUserUseCase,
                               IRefreshTokenUseCase refreshTokenUseCase)
        {
            _mapper = mapper;
            _addBookToUserByIsbnUseCase = addBookToUserByIsbnUseCase;
            _addBookToUserByTitleAuthorUseCase = addBookToUserByTitleAuthorUseCase;
            _getUserBooksUseCase = getUserBooksUseCase;
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost("Register")]
        public async Task <ActionResult> Register([FromBody]RegisterUserRequest request)
        {

                await _registerUserUseCase.ExecuteAsync(request.Username, request.Email, request.Password);

                return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginUserRequest request)
        {

                var jwtToken = await _loginUserUseCase.ExecuteAsync(request.Email, request.Password);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                HttpContext.Response.Cookies.Append("tasty-cookies", jwtToken, cookieOptions);

                if (request.Email == "adminmail")
                    HttpContext.Response.Cookies.Append("IsAdmin", "Yes", cookieOptions);
                else
                    HttpContext.Response.Cookies.Append("IsAdmin", "No", cookieOptions);

                HttpContext.Response.Cookies.Append("UserEmail", request.Email, cookieOptions);

                return Ok();

        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken()
        {
            var email = HttpContext.Request.Cookies["UserEmail"];
            var jwtToken = await _refreshTokenUseCase.ExecuteAsync(email);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            HttpContext.Response.Cookies.Delete("tasty-cookies");
            HttpContext.Response.Cookies.Append("tasty-cookies", jwtToken, cookieOptions);

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

                var books = await _getUserBooksUseCase.ExecuteAsync(email);

                var response = books.Select(b => _mapper.Map<BooksResponse>(b));

                return Ok(response);
        }

        [Authorize]
        [HttpPost("AddBookByISBN")]
        public async Task<ActionResult> AddBookByISBN(int isbn)
        {

                var email = HttpContext.Request.Cookies["UserEmail"];

                await _addBookToUserByIsbnUseCase.ExecuteAsync(isbn, email);

                return Ok();
        }

        [Authorize]
        [HttpPost("AddBookByTitleAndAuthor")]
        public async Task<ActionResult> AddBookByTitleAndAuthor(string title, string authorName)
        {

                var email = HttpContext.Request.Cookies["UserEmail"];

                await _addBookToUserByTitleAuthorUseCase.ExecuteAsync(title, authorName, email);

                return Ok();
        }



    }
}
