using EFT.JwtBasic.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserName).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();

            builder.Property(x => x.Password).HasMaxLength(20).IsRequired();

            builder.Property(x => x.Name).HasMaxLength(40);

            builder.HasMany(x => x.AppUserRoles).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        
    }
}
