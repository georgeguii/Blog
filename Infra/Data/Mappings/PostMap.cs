using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(256)
            .IsRequired();


        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastUpdate)
            .HasColumnType("datetime");

        builder.HasOne(e => e.CreatedBy)
            .WithMany(c => c.MyPosts)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
