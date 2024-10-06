using Library_API.Application.Interfaces;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;
using Library_API.Core.Abstractions;
using Library_API.Core.Models;

namespace Library_API.Application.UseCases.UserUseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserUseCase(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task ExecuteAsync(string userName, string email, string password)
        {
            var testUser = await _usersRepository.GetByEmail(email);
            if (testUser != null)
            {
                throw new Exception("User with this email already exists"); 
            }
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _usersRepository.Create(user);
        }
    }
}
