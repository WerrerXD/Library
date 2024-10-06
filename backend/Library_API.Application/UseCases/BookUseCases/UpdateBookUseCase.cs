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
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;

        public UpdateBookUseCase(IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public async Task ExecuteAsync(Book book)
        {
            bool isExist = await _booksRepository.IsExist(book.Id);
            if (!isExist)
            {
                throw new Exception("Book does not exist");
            }
            isExist = await _authorsRepository.IsExist(book.AuthorId);
            if (!isExist)
            {
                throw new Exception("Author does not exist");
            }

            await _booksRepository.Update(book);
        }
    }
}
