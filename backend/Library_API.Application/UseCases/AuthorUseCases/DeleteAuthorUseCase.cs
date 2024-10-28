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
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            bool isExist = await _unitOfWork.AuthorsRepository.IsExist(id);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }
            var author = await _unitOfWork.AuthorsRepository.GetById(id);
            await _unitOfWork.AuthorsRepository.Delete(author);
            await _unitOfWork.Save();
        }
    }
}
