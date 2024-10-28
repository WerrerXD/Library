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
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(Author author)
        {
            bool isExist = await _unitOfWork.AuthorsRepository.IsExist(author.Id);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }
            await _unitOfWork.AuthorsRepository.Update(author);
            await _unitOfWork.Save();
        }
    }
}
