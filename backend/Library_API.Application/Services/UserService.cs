using Library_API.Core.Models;
using Library_API.DataAccess;
using Library_API.DataAccess.Repositories;
using Library_API.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _usersRepository = usersRepository;
        }

        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {

            var user = await _usersRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                return null;
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task AddBookByISBN(int isbn, string email)
        {
             await _usersRepository.AddBookByISBN(isbn, email);
        }

        public async Task<List<Book>> GetUserBooks(string email)
        {
            return await _usersRepository.GetBooks(email);
        }

        public async Task AddBookByTitleAndAuthor(string title, string authorName, string email)
        {
            await _usersRepository.AddBookByTitleAndAuthor(title, authorName, email);
        }
    }
}
