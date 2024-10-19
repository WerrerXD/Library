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
        private readonly IUsersRepository _usersRepository;
        private readonly IBooksRepository _booksRepository;

        public AddBookToUserByTitleAuthorUseCase(IUsersRepository usersRepository, IBooksRepository booksRepository)
        {
            _usersRepository = usersRepository;
            _booksRepository = booksRepository;
        }

        public async Task ExecuteAsync(string title, string authorLastName, string email)
        {
            _ = await _usersRepository.GetByEmail(email) ?? throw new NotFoundException("User does not exist");
            if (!await _booksRepository.IsExistByTitleAuthor(title, authorLastName))
                throw new NotFoundException("Book does not exist");
            var count = await _usersRepository.AddBookByTitleAndAuthor(title, authorLastName, email);
            if (count == 0)
            {
                throw new NotFoundException("Book that you are looking for is taken");
            }
        }
    }
}
