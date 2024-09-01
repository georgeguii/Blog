using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Email).HasIndex(x => x.Address).IsUnique();
        builder.HasIndex(x => x.Nickname).IsUnique();
        builder.OwnsOne(x => x.Document).HasIndex(x => x.Text).IsUnique();

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("Password")
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

        builder.OwnsOne(x => x.Document)
            .Property(x => x.Text)
            .HasColumnType("char")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.IsDisabled)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastUpdate)
            .HasColumnType("datetime");
    }
}