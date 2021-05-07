using AutoMapper;
using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using EFT.JwtBasic.WebApi.CustomFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFT.JwtBasic.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService jwtService;
        private readonly IAppUserService appUserService;
        private readonly IMapper mapper;
        public AuthController(IJwtService jwtService, IAppUserService appUserService, IMapper mapper)
        {
            this.jwtService = jwtService;
            this.appUserService = appUserService;
            this.mapper = mapper;
        }
       [HttpGet("[action]")]
       public async Task<IActionResult> SignIn([FromQuery]AppUserLoginDto appUserLoginDto)
        {
            var appUser = await this.appUserService.FindByUserNameAsync(appUserLoginDto.UserName);

            if (appUser == null)
            {
                return BadRequest("Username or password is invalid");
            }

            if(await this.appUserService.CheckPasswordAsync(appUserLoginDto))
            {
                var roles = await this.appUserService.GetRolesByUserName(appUserLoginDto.UserName);
                var token =this.jwtService.GenerateJwt(appUser,roles);
                return Created("", token);
            }

            return BadRequest("Username or password is invalid");  
        }
        [HttpGet("[action]")]
        [ValidModel]
        public async Task<IActionResult> Register([FromQuery]AppUserAddDto appUserAddDto)
        {
            var user = await this.appUserService.FindByUserNameAsync(appUserAddDto.Name);
            if (user != null)
            {
                return BadRequest($"{appUserAddDto.UserName} is already registered in the system");
            }
            await this.appUserService.Add(this.mapper.Map<AppUser>(appUserAddDto));
            return Created("",appUserAddDto);
        }
    }
}
