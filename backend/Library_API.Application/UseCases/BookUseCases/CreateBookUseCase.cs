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
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;

        public CreateBookUseCase(IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public async Task<Guid> ExecuteAsync(Book book)
        {
            bool isExist = await _booksRepository.IsExistByTitleAuthor(book.Title, book.AuthorName);
            if (isExist)
            {
                throw new AlreadyExistsException("Book already exists");
            }
            isExist = await _authorsRepository.IsExist(book.AuthorId);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }
            var id =  await _booksRepository.Create2(book, book.AuthorId);
            await _booksRepository.Save();
            return id;
        }
    }
}
