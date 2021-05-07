using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser> , IAppUserService
    {
        private readonly IAppUserDal appUserDal;
        public AppUserManager(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) :base(genericDal)
        {
            this.appUserDal = appUserDal;
        }

        public async Task<bool> CheckPasswordAsync(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await this.appUserDal.GetByFiter(x => x.UserName == appUserLoginDto.UserName);
            return appUser.Password == appUserLoginDto.Password ? true : false;
        }

        public async Task<AppUser> FindByUserNameAsync(string userName)
        {
            return await this.appUserDal.GetByFiter(x => x.UserName == userName);
        }

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            return await this.appUserDal.GetRolesByUserName(userName);
        }
    }
}
