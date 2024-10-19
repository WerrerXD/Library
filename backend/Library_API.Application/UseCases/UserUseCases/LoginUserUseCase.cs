using Library_API.Application.Exceptions;
using Library_API.Application.Interfaces;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.UserUseCases
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserUseCase(IUsersRepository usersRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> ExecuteAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new BadRequestException("User data can not be empty");
            var user = await _usersRepository.GetByEmail(email)?? throw new NotFoundException("User does not exist");

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new UnauthorizedException("Wrong password");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
