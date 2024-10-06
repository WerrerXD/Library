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
    public class GetAllAuthorsUseCase: IGetAllAuthorsUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public GetAllAuthorsUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<List<Author>> ExecuteAsync()
        {
            return await _authorsRepository.GetAll();
        }
    }
}
