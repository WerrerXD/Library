using Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.AuthorUseCases
{
    public class GetAuthorsBooksUseCase : IGetAuthorsBooksUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public GetAuthorsBooksUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<List<Book>> ExecuteAsync(string lastname)
        {
            return await _authorsRepository.GetBooks(lastname);
        }
    }
}
