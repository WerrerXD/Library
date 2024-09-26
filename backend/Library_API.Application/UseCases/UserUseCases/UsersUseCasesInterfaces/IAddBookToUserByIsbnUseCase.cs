namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface IAddBookToUserByIsbnUseCase
    {
        Task ExecuteAsync(int isbn, string email);
    }
}