using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Email)
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .HasMaxLength(50);

            builder.Property(p => p.ImagePath)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.FolderPath)
                .HasMaxLength(100);

            builder.Property(p => p.CreateAt)
                .IsRequired();
        }
    }
}
