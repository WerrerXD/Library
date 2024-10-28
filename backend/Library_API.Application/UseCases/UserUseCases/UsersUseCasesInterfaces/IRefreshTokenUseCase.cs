namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface IRefreshTokenUseCase
    {
        Task<string> ExecuteAsync(string email);
    }
}