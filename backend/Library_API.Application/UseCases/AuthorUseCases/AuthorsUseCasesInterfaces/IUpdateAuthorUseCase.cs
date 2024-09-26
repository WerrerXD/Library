namespace Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces
{
    public interface IUpdateAuthorUseCase
    {
        Task<Guid> ExecuteAsync(Guid id, string userName, string lastName, DateOnly dateOfBirth, string country);
    }
}