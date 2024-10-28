using Library_API.Application.Exceptions;
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
    public class GetUserBooksUseCase : IGetUserBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserBooksUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<List<Book>> ExecuteAsync(string email)
        {
            _ = await _unitOfWork.UsersRepository.GetByEmail(email) ?? throw new NotFoundException("User does not exist");
            return await _unitOfWork.UsersRepository.GetBooks(email);
        }
    }
}
