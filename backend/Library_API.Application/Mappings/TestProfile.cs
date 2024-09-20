using AutoMapper;
using Library_API.Core.Contracts;
using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
