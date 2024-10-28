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
        private readonly IUnitOfWork _unitOfWork;

        public GetBookByIsbnUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<Book> ExecuteAsync(double isbn)
        {
            var book = await _unitOfWork.BooksRepository.GetByISBN(isbn)?? throw new NotFoundException("Book does not exist");
            return book;
        }
    }
}

