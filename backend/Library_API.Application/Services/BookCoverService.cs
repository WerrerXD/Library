using Library_API.Application.Interfaces;
using Library_API.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.Services
{
    public class BookCoverService : IBookCoverService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IFileStorageService _fileStorageService;

        public BookCoverService(IBooksRepository booksRepository, IFileStorageService fileStorageService)
        {
            _booksRepository = booksRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<string> AddCoverToBookAsync(Guid bookId, IFormFile coverPhoto)
        {
            var book = await _booksRepository.GetById(bookId) ?? throw new Exception("Book does not exist");

            var coverUrl = await _fileStorageService.SaveFileAsync(coverPhoto, "bookscovers");

            book.CoverImageUrl = coverUrl;

            return coverUrl;
        }
    }
}
