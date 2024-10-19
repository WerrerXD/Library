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
    public class GetBookByIsbnUseCase : IGetBookByIsbnUseCase
    {
        private readonly IBooksRepository _booksRepository;

        public GetBookByIsbnUseCase(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book> ExecuteAsync(int isbn)
        {
            var book = await _booksRepository.GetByISBN(isbn)?? throw new NotFoundException("Book does not exist");
            return book;
        }
    }
}

