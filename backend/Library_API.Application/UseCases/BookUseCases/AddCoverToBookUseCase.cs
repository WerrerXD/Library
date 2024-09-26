using Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using Library_API.DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.BookUseCases
{
    public class AddCoverToBookUseCase : IAddCoverToBookUseCase
    {
        private readonly IBooksRepository _booksRepository;

        public AddCoverToBookUseCase(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<string> ExecuteAsync(Guid id, IFormFile CoverPhoto, string serverFolder, string folder)
        {
            var book = await _booksRepository.GetById(id);

            if (book == null)
            {
                return null;
            }

            if (CoverPhoto != null)
            {
                book.CoverImageUrl = "/" + folder;

                await CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }

            return book.CoverImageUrl;
        }
    }
}
