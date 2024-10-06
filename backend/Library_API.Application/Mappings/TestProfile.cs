using AutoMapper;
using Library_API.Application.Contracts;
using Library_API.Core.Models;

namespace Library_API.Application.Mappings
{
    public class TestProfile: Profile
    {
        public TestProfile() 
        {
            CreateMap<Book, BooksResponse>();

            CreateMap<Author, AuthorsResponse>();

            CreateMap<AuthorsRequest, Author>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<BooksRequest, Book>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        }
    }
}
