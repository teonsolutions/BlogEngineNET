using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class RolStatusEntityTypeConfiguration : IEntityTypeConfiguration<RolStatus>
    {
        public void Configure(EntityTypeBuilder<RolStatus> builder)
        {
            builder.ToTable("RolStatus", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RolStatusID").ValueGeneratedOnAdd();

            builder.Property(x => x.RolID).HasColumnName("RolID").IsRequired();
            builder.Property(x => x.StatusID).HasColumnName("StatusID").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();

            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
