using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Configurations
{
    public class ErrorConfiguration : BaseConfiguration<Error>
    {
        public override void Configure(EntityTypeBuilder<Error> builder)
        {
            base.Configure(builder);

            builder.ToTable("error");
            
            builder.Property(b => b.Name)
                .HasMaxLength(255);
        }
    }
}