using Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces;
using Library_API.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.AuthorUseCases
{
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public UpdateAuthorUseCase(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<Guid> ExecuteAsync(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country)
        {
            return await _authorsRepository.Update(id, userName, lastName, dateOfBirth, country);
        }
    }
}
