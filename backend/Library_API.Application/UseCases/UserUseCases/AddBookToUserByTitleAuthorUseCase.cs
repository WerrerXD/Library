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

        public AddBookToUserByTitleAuthorUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(string title, string authorLastName, string email)
        {
            await _usersRepository.AddBookByTitleAndAuthor(title, authorLastName, email);
        }
    }
}
