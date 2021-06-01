using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BS.Infrastructure.Data.EntityConfiguration
{
    public partial class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            // table
            builder.ToTable("BookCategory", "dbo");

            // key
            builder.HasKey(t => t.Id); builder.Property(t => t.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(t => t.Title).HasColumnName("Title").HasColumnType("nvarchar(150)").HasMaxLength(150).IsRequired();
            builder.Property(t => t.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired();
            builder.Property(t => t.LastModifiedBy).HasColumnName("LastModifiedBy").HasColumnType("uniqueidentifier");
            builder.Property(t => t.LastModifiedAt).HasColumnName("LastModifiedAt").HasColumnType("datetime");
            builder.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder").HasColumnType("int").IsRequired();
            builder.Property(t => t.IsActive).HasColumnName("IsActive").HasColumnType("bit").IsRequired();
            builder.Property(t => t.SoftDeleted).HasColumnName("SoftDeleted").HasColumnType("bit").IsRequired();
            builder.Property(t => t.RowVersion).HasColumnName("RowVersion").IsConcurrencyToken();
        }
    }
}
