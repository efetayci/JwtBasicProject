using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
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
        public AuthController(IJwtService jwtService, IAppUserService appUserService)
        {
            this.jwtService = jwtService;
            this.appUserService = appUserService;
        }
       [HttpGet("[action]")]
       public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await this.appUserService.FindByUserNameAsync(appUserLoginDto.UserName);

            if (appUser == null)
            {
                return BadRequest("Username or password is invalid");
            }

            if(await this.appUserService.CheckPasswordAsync(appUserLoginDto))
            {
                var token =this.jwtService.GenerateJwt(appUser, null);
                return Created("", token);
            }

            return Ok();
            
        }
    }
}
