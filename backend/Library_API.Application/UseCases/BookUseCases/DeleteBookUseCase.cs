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
                throw new NotFoundException("Book does not exist");
            }
            var book = await _booksRepository.GetById(id);
            await _booksRepository.Delete(book);
            await _booksRepository.Save();
        }
    }
}
