﻿using Library_API.Core.Models;

namespace Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces
{
    public interface ICreateAuthorUseCase
    {
        Task<Guid> ExecuteAsync(Author author);
    }
}