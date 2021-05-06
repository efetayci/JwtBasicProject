using EFT.JwtBasic.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

            builder.HasMany(x => x.AppUserRoles).WithOne(x => x.AppRole).HasForeignKey(x => x.AppRoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
