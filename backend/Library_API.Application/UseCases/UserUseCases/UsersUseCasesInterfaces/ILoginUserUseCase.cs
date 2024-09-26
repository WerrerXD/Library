namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface ILoginUserUseCase
    {
        Task<string> ExecuteAsync(string email, string password);
    }
}