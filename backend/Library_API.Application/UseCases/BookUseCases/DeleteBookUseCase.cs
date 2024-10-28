using Library_API.Application.Exceptions;
using Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.BookUseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            bool isExist = await _unitOfWork.BooksRepository.IsExist(id);
            if (!isExist)
            {
                throw new NotFoundException("Book does not exist");
            }
            var book = await _unitOfWork.BooksRepository.GetById(id);
            await _unitOfWork.BooksRepository.Delete(book);
            await _unitOfWork.Save();
        }
    }
}
