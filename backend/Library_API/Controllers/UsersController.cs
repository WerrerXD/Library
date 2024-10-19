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

        private readonly IMapper _mapper;
        public UsersController(
                               IMapper mapper,
                               IAddBookToUserByIsbnUseCase addBookToUserByIsbnUseCase,
                               IAddBookToUserByTitleAuthorUseCase addBookToUserByTitleAuthorUseCase,
                               IGetUserBooksUseCase getUserBooksUseCase,
                               ILoginUserUseCase loginUserUseCase,
                               IRegisterUserUseCase registerUserUseCase)
        {
            _mapper = mapper;
            _addBookToUserByIsbnUseCase = addBookToUserByIsbnUseCase;
            _addBookToUserByTitleAuthorUseCase = addBookToUserByTitleAuthorUseCase;
            _getUserBooksUseCase = getUserBooksUseCase;
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
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

                var token = await _loginUserUseCase.ExecuteAsync(request.Email, request.Password);

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
