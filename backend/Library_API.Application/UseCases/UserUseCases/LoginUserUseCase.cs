using Library_API.Application.Contracts;
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

        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        private readonly IUnitOfWork _unitOfWork;

        public LoginUserUseCase(IUnitOfWork unitofwork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _unitOfWork = unitofwork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> ExecuteAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new BadRequestException("User data can not be empty");
            var user = await _unitOfWork.UsersRepository.GetByEmail(email)?? throw new NotFoundException("User does not exist");

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new UnauthorizedException("Wrong password");
            }
           
            var jwtToken = _jwtProvider.GenerateToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();
            

            RefreshTokenModel refreshTokenModel = new()
            {
                Id = Guid.NewGuid(),
                RefreshToken = refreshToken,
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(1)
            };

            await _unitOfWork.RefreshTokensRepository.Create(refreshTokenModel);
            await _unitOfWork.Save();

            return jwtToken;
        }
    }
}
