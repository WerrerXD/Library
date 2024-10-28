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

    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdUseCase(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        public async Task<Author?> ExecuteAsync(Guid id)
        {
            var author = await _unitOfWork.AuthorsRepository.GetById(id) ?? throw new NotFoundException("Author does not exist");
            return author;
        }
    }

}
