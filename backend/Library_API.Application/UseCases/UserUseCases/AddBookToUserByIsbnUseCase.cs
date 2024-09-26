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

        public AddBookToUserByIsbnUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(int isbn, string email)
        {
            await _usersRepository.AddBookByISBN(isbn, email);
        }
    }
}
