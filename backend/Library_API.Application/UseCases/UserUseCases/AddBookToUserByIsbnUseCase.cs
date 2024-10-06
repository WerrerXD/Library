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
        private readonly IUsersRepository _usersRepository;
        private readonly IBooksRepository _booksRepository;

        public AddBookToUserByIsbnUseCase(IUsersRepository usersRepository, IBooksRepository booksRepository)
        {
            _usersRepository = usersRepository;
            _booksRepository = booksRepository;
        }

        public async Task ExecuteAsync(int isbn, string email)
        {
            _ = await _usersRepository.GetByEmail(email) ?? throw new Exception("User does not exist");
            _ = await _booksRepository.GetByISBN(isbn) ?? throw new Exception("Book does not exist");
            var count = await _usersRepository.AddBookByISBN(isbn, email);
            if (count == 0)
            {
                throw new Exception("Book that you are looking for is taken");
            }
        }
    }
}
