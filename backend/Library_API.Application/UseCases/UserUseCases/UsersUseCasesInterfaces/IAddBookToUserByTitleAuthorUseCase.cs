namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface IAddBookToUserByTitleAuthorUseCase
    {
        Task ExecuteAsync(string title, string authorLastName, string email);
    }
}