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
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<Guid> ExecuteAsync(Author author)
        {
            bool isExist = await _unitOfWork.AuthorsRepository.IsExistByName(author.UserName, author.LastName);
            if (isExist)
            {
                throw new AlreadyExistsException("Author already exists");
            }

            var id =  await _unitOfWork.AuthorsRepository.Create(author);
            await _unitOfWork.Save();
            return id;
        }
    }
}
