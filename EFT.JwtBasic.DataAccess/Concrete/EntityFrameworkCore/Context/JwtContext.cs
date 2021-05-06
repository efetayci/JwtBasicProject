using EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using EFT.JwtBasic.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class JwtContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-LOIEK0I;database=JwtBasic;Integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration( new AppUserConfiguration());
            modelBuilder.ApplyConfiguration( new AppUserRoleConfiguration());
            modelBuilder.ApplyConfiguration( new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
