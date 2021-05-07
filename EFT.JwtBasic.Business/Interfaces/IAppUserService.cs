using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserNameAsync(string username);
        Task<bool> CheckPasswordAsync(AppUserLoginDto appUserLoginDto);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
