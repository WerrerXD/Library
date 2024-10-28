using Library_API.Application.Contracts;
using Library_API.Application.Exceptions;
using Library_API.Application.Interfaces;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;
using Library_API.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.UserUseCases
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenUseCase(IJwtProvider jwtProvider, IUnitOfWork unitOfWork)
        {
            _jwtProvider = jwtProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ExecuteAsync(string email)
        {
            var user = await _unitOfWork.UsersRepository.GetByEmail(email)
                ?? throw new NotFoundException("User does not exist");

            var refreshToken = await _unitOfWork.RefreshTokensRepository.GetNotExpiredToken(user.Id)
                ?? throw new UnauthorizedException("Your refresh token has expired, pls log in again");

            var newJwtToken = _jwtProvider.GenerateToken(user);

            return newJwtToken;
        }
    }
}
