namespace Library_API.Contracts
{
    public record AuthorsRequest
    (
        string UserName,
        string LastName,
        DateOnly DateOfBirth,
        string Country
    );
}
