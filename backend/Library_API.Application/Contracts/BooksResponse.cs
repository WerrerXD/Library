﻿using Library_API.Core.Models;

namespace Library_API.Application.Contracts
{
    public class BooksResponse: BooksDTO
    {
        public required Guid Id { get; set; }
    }
}
