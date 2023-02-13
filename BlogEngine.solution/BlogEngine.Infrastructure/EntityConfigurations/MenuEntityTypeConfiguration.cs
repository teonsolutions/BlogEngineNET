using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogEngine.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Infrastructure.EntityConfigurations
{
    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("MenuID").ValueGeneratedOnAdd();

            builder.Property(x => x.MenuName).HasColumnName("MenuName").IsRequired();
            builder.Property(x => x.MenuBaseID).HasColumnName("MenuBaseID").IsRequired(false);
            builder.Property(x => x.Url).HasColumnName("Url").IsRequired(false);
            builder.Property(x => x.Guid).HasColumnName("Guid").IsRequired();

            builder.Property(x => x.CreationUser).HasColumnName("CreationUser").IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
            builder.Property(x => x.UpdateUser).HasColumnName("UpdateUser").IsRequired(false);
            builder.Property(x => x.UpdateDate).HasColumnName("UpdateDate").IsRequired(false);
        }
    }
}
