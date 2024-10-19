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
    public class CreateAuthorUseCase : ICreateAuthorUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public CreateAuthorUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<Guid> ExecuteAsync(Author author)
        {
            bool isExist = await _authorsRepository.IsExistByName(author.UserName, author.LastName);
            if (isExist)
            {
                throw new AlreadyExistsException("Author already exists");
            }

            var id =  await _authorsRepository.Create(author);
            await _authorsRepository.Save();
            return id;
        }
    }
}
