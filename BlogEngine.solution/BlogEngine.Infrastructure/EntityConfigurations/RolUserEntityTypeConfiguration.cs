using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class RolUserEntityTypeConfiguration : IEntityTypeConfiguration<RolUser>
    {
        public void Configure(EntityTypeBuilder<RolUser> builder)
        {
            builder.ToTable("RolUser", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RolUserID").ValueGeneratedOnAdd();

            builder.Property(x => x.RolID).HasColumnName("RolID").IsRequired();
            builder.Property(x => x.UserID).HasColumnName("UserID").IsRequired();

            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
