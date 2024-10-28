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
    public class AddBookToUserByIsbnUseCase : IAddBookToUserByIsbnUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBookToUserByIsbnUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(int isbn, string email)
        {
            _ = await _unitOfWork.UsersRepository.GetByEmail(email) ?? throw new NotFoundException("User does not exist");
            _ = await _unitOfWork.BooksRepository.GetByISBN(isbn) ?? throw new NotFoundException("Book does not exist");
            var count = await _unitOfWork.UsersRepository.AddBookByISBN(isbn, email);
            if (count == 0)
            {
                throw new NotFoundException("Book that you are looking for is taken");
            }
        }
    }
}
