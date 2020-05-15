using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class MessageMap : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MessageEntity> builder)
        {
            builder.ToTable("Message");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.IdChat)
                .IsRequired();

            builder.Property(p => p.IdUser)
                .IsRequired();

            builder.Property(p => p.Text)
                .IsRequired();

            builder.Property(p => p.Time)
                .IsRequired();
        }
    }
}
