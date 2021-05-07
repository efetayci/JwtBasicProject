using EFT.JwtBasic.Business.Concrete;
using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Business.Validation.FluentValidation;
using EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using EFT.JwtBasic.Entites.Dtos.ProductDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.DependencyResolves.Microsoftioc
{
    public static class CustomExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<IProductDal, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IAppRoleDal, EfAppRoleRepository>();
            services.AddScoped<IAppRoleService, AppRoleManager>();

            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();

            services.AddScoped<IAppUserRoleDal, EfAppUserRoleRepository>();
            services.AddScoped<IAppUserRoleService, AppUserRoleManager>();
            //Token için
            services.AddScoped<IJwtService, JwtManager>();

            //validation için
            services.AddTransient<IValidator<ProductAddDto>, ProductAddDtoValidator>();
            services.AddTransient<IValidator<ProductUpdateDto>,ProductUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>,AppUserLognDtoValidator>();
            services.AddTransient<IValidator<AppUserAddDto>,AppUserAddDtoValidator>();

            

            
        }
    }
}
