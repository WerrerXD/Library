using Library_API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.DataAccess.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.
                HasMany(a => a.AuthorBooks)
                .WithOne(b => b.Author);

            builder.Property(a => a.UserName)
               .IsRequired();

            builder.Property(a => a.LastName)
               .IsRequired();

            builder.Property(a => a.DateOfBirth)
               .IsRequired();

            builder.Property(a => a.Country)
               .IsRequired();
        }
    }
}
