using Library_API.Application.Exceptions;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;
using Library_API.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.UserUseCases
{
    public class AddBookToUserByTitleAuthorUseCase : IAddBookToUserByTitleAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBookToUserByTitleAuthorUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(string title, string authorLastName, string email)
        {
            _ = await _unitOfWork.UsersRepository.GetByEmail(email) ?? throw new NotFoundException("User does not exist");
            if (!await _unitOfWork.BooksRepository.IsExistByTitleAuthor(title, authorLastName))
                throw new NotFoundException("Book does not exist");
            var count = await _unitOfWork.UsersRepository.AddBookByTitleAndAuthor(title, authorLastName, email);
            if (count == 0)
            {
                throw new NotFoundException("Book that you are looking for is taken");
            }
        }
    }
}
