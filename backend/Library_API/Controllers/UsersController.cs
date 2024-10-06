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
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
                return BadRequest("User data can not be empty");
            try
            {
                await _registerUserUseCase.ExecuteAsync(request.Username, request.Email, request.Password);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("User data can not be empty");
            try
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
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
            try
            {
                var email = HttpContext.Request.Cookies["UserEmail"];

                var books = await _getUserBooksUseCase.ExecuteAsync(email);

                var response = books.Select(b => _mapper.Map<BooksResponse>(b));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AddBookByISBN")]
        public async Task<ActionResult> AddBookByISBN(int isbn)
        {
            try
            {
                var email = HttpContext.Request.Cookies["UserEmail"];

                await _addBookToUserByIsbnUseCase.ExecuteAsync(isbn, email);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AddBookByTitleAndAuthor")]
        public async Task<ActionResult> AddBookByTitleAndAuthor(string title, string authorName)
        {
            try
            {
                var email = HttpContext.Request.Cookies["UserEmail"];

                await _addBookToUserByTitleAuthorUseCase.ExecuteAsync(title, authorName, email);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}
