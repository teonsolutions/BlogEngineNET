using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("StatusID").ValueGeneratedOnAdd();

            builder.Property(x => x.Description).HasColumnName("Description").IsRequired();

            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
