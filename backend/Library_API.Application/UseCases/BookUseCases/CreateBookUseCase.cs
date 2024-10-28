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
    public class CreateBookUseCase : ICreateBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<Guid> ExecuteAsync(Book book)
        {
            bool isExist = await _unitOfWork.BooksRepository.IsExistByTitleAuthor(book.Title, book.AuthorName);
            if (isExist)
            {
                throw new AlreadyExistsException("Book with this title and author already exists");
            }
            isExist = await _unitOfWork.BooksRepository.IsExistByIsbn(book.ISBN);
            if (isExist)
            {
                throw new AlreadyExistsException("Book with this isbn already exists");
            }
            isExist = await _unitOfWork.AuthorsRepository.IsExist(book.AuthorId);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }
            var id =  await _unitOfWork.BooksRepository.Create(book);
            await _unitOfWork.Save();
            return id;
        }
    }
}
