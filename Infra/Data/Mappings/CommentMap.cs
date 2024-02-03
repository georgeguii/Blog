using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mappings;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(p => p.Description)
            .Property(p => p.Text)
            .HasColumnType("varchar")
            .HasMaxLength(256)
            .IsRequired();


        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastUpdate)
            .HasColumnType("datetime");

        builder.HasOne(e => e.CreatedBy)
            .WithMany(c => c.MyComments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasOne(e => e.OnPost)
            .WithMany(c => c.Comments)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.RelatedComment)
            .WithMany(c => c.ChildrenComments)
            .HasForeignKey(e => e.CommentId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(false);
    }
}
