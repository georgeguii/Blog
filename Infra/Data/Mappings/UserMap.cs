using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Nickname).IsUnique();
        builder.HasIndex(x => x.Cpf).IsUnique();

        builder.Property(x => x.Email)
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnType("varchar")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Nickname)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .HasColumnType("varchar")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastUpdate)
            .HasColumnType("datetime");
    }
}
