using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("PostID").ValueGeneratedOnAdd();

            builder.Property(x => x.Title).HasColumnName("Title").IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").IsRequired();
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
            builder.Property(x => x.PublishedDate).HasColumnName("PublishedDate").IsRequired(false);
            builder.Property(x => x.RejectedDate).HasColumnName("RejectedDate").IsRequired(false);
            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
