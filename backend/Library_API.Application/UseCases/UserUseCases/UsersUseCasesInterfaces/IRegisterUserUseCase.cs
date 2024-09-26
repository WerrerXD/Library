namespace Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces
{
    public interface IRegisterUserUseCase
    {
        Task ExecuteAsync(string userName, string email, string password);
    }
}