using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class RolEntityTypeConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RolID").ValueGeneratedOnAdd();

            builder.Property(x => x.RolName).HasColumnName("RolName").IsRequired();
            builder.Property(x => x.Guid).HasColumnName("Guid").IsRequired();

            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
