using AutoMapper;
using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Business.StringInfos;
using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using EFT.JwtBasic.Entites.Token;
using EFT.JwtBasic.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
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
       [HttpPost("[action]")]
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
                JwtAccessToken Token = new JwtAccessToken
                {
                    Token = token
                };
                return Created("", Token);
            }

            return BadRequest("Username or password is invalid");  
        }
        [HttpGet("[action]")]
        [ValidModel]
        public async Task<IActionResult> Register([FromQuery]AppUserAddDto appUserAddDto,
            [FromServices] IAppUserRoleService appUserRoleService, 
            [FromServices] IAppRoleService appRoleService)
        {
            var user = await this.appUserService.FindByUserNameAsync(appUserAddDto.Name);
            if (user != null)
            {
                return BadRequest($"{appUserAddDto.UserName} is already registered in the system");
            }
            await this.appUserService.Add(this.mapper.Map<AppUser>(appUserAddDto));

            var appuser = await this.appUserService.FindByUserNameAsync(appUserAddDto.Name);
            var role = await appRoleService.FindByNameAsync(RoleInfo.Member);

            await appUserRoleService.Add(new AppUserRole
            {
                AppUserId = appuser.Id,
                AppRoleId = role.Id
            });

            return Created("",appUserAddDto);
        }

        
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await this.appUserService.FindByUserNameAsync(User.Identity.Name);
            var roles = await this.appUserService.GetRolesByUserName(User.Identity.Name);

            AppUserDto appUserDto = new AppUserDto
            {
                Name = user.Name,
                Roles = roles.Select(x => x.Name).ToList(),
                UserName = user.UserName
            };

            return Ok(appUserDto);
        }

    }
}
