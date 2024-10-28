using Library_API.Application.Exceptions;
using Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Application.UseCases.AuthorUseCases
{
    public class GetAuthorsBooksUseCase : IGetAuthorsBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorsBooksUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<List<Book>> ExecuteAsync(string Name, string LastName)
        {
            bool isExist = await _unitOfWork.AuthorsRepository.IsExistByName(Name, LastName);
            if (!isExist)
            {
                throw new NotFoundException("Author does not exist");
            }
            var books = await _unitOfWork.AuthorsRepository.GetBooks(Name, LastName);
            return books;
        }
    }
}
