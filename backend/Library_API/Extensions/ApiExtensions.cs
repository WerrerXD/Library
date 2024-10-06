using Library_API.Application.UseCases.AuthorUseCases;
using Library_API.Application.UseCases.AuthorUseCases.AuthorsUseCasesInterfaces;
using Library_API.Application.UseCases.BookUseCases;
using Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces;
using Library_API.Application.UseCases.UserUseCases;
using Library_API.Application.UseCases.UserUseCases.UsersUseCasesInterfaces;
using Library_API.AuthorizeRequirements;
using Library_API.DataAccess;
using Library_API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library_API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.Requirements.Add(new AdminRequirement("IsAdmin"));
                });
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();


            services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IGetAuthorsBooksUseCase, GetAuthorsBooksUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();

            services.AddScoped<IAddBookToUserByIsbnUseCase, AddBookToUserByIsbnUseCase>();
            services.AddScoped<IAddBookToUserByTitleAuthorUseCase, AddBookToUserByTitleAuthorUseCase>();
            services.AddScoped<IGetUserBooksUseCase, GetUserBooksUseCase>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        }
    }
}
