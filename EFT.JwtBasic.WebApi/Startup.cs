using EFT.JwtBasic.Business.DependencyResolves.Microsoftioc;
using EFT.JwtBasic.Business.StringInfos;
using EFT.JwtBasic.WebApi.CustomFilters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EFT.JwtBasic.WebApi.Mapping.AutoMapperProfile;
using Microsoft.OpenApi.Models;
using EFT.JwtBasic.Business.Interfaces;

namespace EFT.JwtBasic.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDependencies(); //DI implementation

            services.AddScoped(typeof(ValidId<>)); //Filter implement by generic

            //added jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer =  JwtInfo.Issuer,
                    ValidAudience = JwtInfo.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecuritKkey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew=TimeSpan.Zero
                };
            });

            //auto mapper 
            services.AddAutoMapper(typeof(Startup));

            //addSwagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Jwt Basic",
                    Description = "Jwt , RestApi , Katmanlý Mimari",
                    Contact = new OpenApiContact
                    {
                        Name = "Efe Taycý",
                        Email = "efefehmitayci@gmail.com",
                        Url = new Uri("https://twitter.com/efetayci")
                    }
                });
            });
       
            services.AddControllers().AddFluentValidation(); //fluent validation geçtiðini belirtmek için
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IAppUserService appUserService, IAppUserRoleService appUserRoleService
         , IAppRoleService appRoleService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //addSwagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseExceptionHandler("/Error"); //Bir hata olunca buraya git. localhost/error

            JwtIdentityInitializer.Seed(appUserService, appUserRoleService, appRoleService).Wait();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //token için authorization üstünde olmalý

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
