using Library_API.Application.Exceptions;
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
                throw new NotFoundException("Author does not exist");
            }
            var author = await _authorsRepository.GetById(id);
            await _authorsRepository.Delete(author);
            await _authorsRepository.Save();
        }
    }
}
