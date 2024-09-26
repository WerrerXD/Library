namespace Library_API.Application.UseCases.BookUseCases.BooksUseCasesInterfaces
{
    public interface IUpdateBookUseCase
    {
        Task<Guid> ExecuteAsync(Guid id, double isbn, string title, string genre, string description, string authorname, DateOnly datein, DateOnly dateout, Guid authorid);
    }
}