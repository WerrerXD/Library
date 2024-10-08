﻿namespace Library_API.Application.Contracts
{
    public class AuthorsDTO
    {
        public required string UserName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Country { get; set; }
    }
}
