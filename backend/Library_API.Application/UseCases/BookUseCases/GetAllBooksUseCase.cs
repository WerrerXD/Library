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
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBooksRepository _booksRepository;

        public GetAllBooksUseCase(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> ExecuteAsync()
        {
            return await _booksRepository.GetAll();
        }
    }
}
