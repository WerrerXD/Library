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
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task ExecuteAsync(Book book)
        {
            bool isExist = await _unitOfWork.BooksRepository.IsExist(book.Id);
            if (!isExist)
            {
                throw new NotFoundException("Book does not exist");
            }
            var testbook = await _unitOfWork.BooksRepository.GetByISBN(book.ISBN);
            var count = await _unitOfWork.BooksRepository.GetCountByISBN(book.ISBN);
            if (count > 1 || (count == 1 && testbook.Id != book.Id))
            {
                throw new AlreadyExistsException("Book with this isbn already exists");
            }
            isExist = await _unitOfWork.AuthorsRepository.IsExist(book.AuthorId);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }

            await _unitOfWork.BooksRepository.Update(book);
            await _unitOfWork.Save();
        }
    }
}
