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
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IBooksRepository _booksRepository;

        public GetBookByIdUseCase(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book> ExecuteAsync(Guid id)
        {
            var book = await _booksRepository.GetById(id)?? throw new Exception("Book does not exist");
            return book;
        }
    }
}
