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

    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public GetAuthorByIdUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<Author?> ExecuteAsync(Guid id)
        {
            var author = await _authorsRepository.GetById(id) ?? throw new Exception("Author does not exist");
            return author;
        }
    }

}
