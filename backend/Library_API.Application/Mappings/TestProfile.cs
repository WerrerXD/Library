using AutoMapper;
using Library_API.Core.Contracts;
using Library_API.Core.Models;

namespace Library_API.Application.Mappings
{
    public class TestProfile: Profile
    {
        public TestProfile() 
        {
            CreateMap<Book, BooksResponse>();

            CreateMap<Author, AuthorsResponse>();
        }
    }
}
