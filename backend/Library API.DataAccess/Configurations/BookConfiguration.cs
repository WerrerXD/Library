using Library_API.Core.Models;
using Library_API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_API.DataAccess.Configurations
{
    public class BookConfiguration: IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder) 
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.AuthorBooks);

            builder.Property(b => b.ISBN)
                .IsRequired();

            builder.Property(b => b.Title)
                .HasMaxLength(Book.MAX_TITLE_LENGTH)
                .IsRequired();

            builder.Property(b => b.Description);

            builder.Property(b => b.AuthorName)
                .IsRequired();

            builder.Property(b => b.Genre)
                .IsRequired();


            builder.Property(b => b.DateIn);

            builder.Property(b => b.DateOut);

            builder.Property(b => b.AuthorId)
                .IsRequired();

            builder.Property(u => u.UserId);
        }
    }
}
