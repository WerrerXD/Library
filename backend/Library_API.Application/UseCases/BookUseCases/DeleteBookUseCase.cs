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
        private readonly IBooksRepository _booksRepository;

        public DeleteBookUseCase(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            bool isExist = await _booksRepository.IsExist(id);
            if (!isExist)
            {
                throw new Exception("Book does not exist");
            }
            await _booksRepository.Delete(id);
        }
    }
}
