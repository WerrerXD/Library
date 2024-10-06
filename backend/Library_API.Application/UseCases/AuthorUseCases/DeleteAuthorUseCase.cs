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
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public DeleteAuthorUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            bool isExist = await _authorsRepository.IsExist(id);
            if (!isExist)
            {
                throw new Exception("Author does not exist");
            }

            await _authorsRepository.Delete(id);
        }
    }
}
