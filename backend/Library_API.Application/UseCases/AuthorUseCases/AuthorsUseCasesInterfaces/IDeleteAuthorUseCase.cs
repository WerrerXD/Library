namespace Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces
{
    public interface IDeleteAuthorUseCase
    {
        Task<Guid> ExecuteAsync(Guid id);
    }
}