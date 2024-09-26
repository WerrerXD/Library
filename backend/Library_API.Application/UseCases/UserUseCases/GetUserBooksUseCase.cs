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
        private readonly IUsersRepository _usersRepository;

        public GetUserBooksUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<Book>> ExecuteAsync(string email)
        {
            return await _usersRepository.GetBooks(email);
        }
    }
}
