using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Business.StringInfos;
using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFT.JwtBasic.WebApi
{
    public static class JwtIdentityInitializer
    {
        public static async Task Seed(IAppUserService appUserService,IAppUserRoleService appUserRoleService
         ,IAppRoleService  appRoleService)
        {
            //Rol varsa eklemicem yoksa eklicem
            var adminRole = await appRoleService.FindByNameAsync(RoleInfo.Admin);
            if (adminRole == null)
            {
                await appRoleService.Add(new AppRole
                {
                    Name = RoleInfo.Admin
                });
            }
            var memberRole = await appRoleService.FindByNameAsync(RoleInfo.Member);
            if (memberRole == null)
            {
                await appRoleService.Add(new AppRole
                {
                    Name = RoleInfo.Member
                });
            }

            //admin kullanıcı eklmedim.
            var adminUser = await appUserService.FindByUserNameAsync("ADMIN");
            if (adminUser == null)
            {
                await appUserService.Add(new AppUser
                {
                    UserName = "ADMIN",
                    Password = "0"
                });
            }
            //appuserrole tablosuna bu ilişkiyi eklemem lazım
            var role = await appRoleService.FindByNameAsync(RoleInfo.Admin);
            var admin = await appUserService.FindByUserNameAsync("ADMIN");
            
            //daha önce eklenmiş mi
            var allUSerRole = await appUserRoleService.GetAll();
            int kontrol = allUSerRole.Where(x => x.AppRoleId == role.Id && x.AppUserId == admin.Id).Count();
            if (kontrol ==0)
            {
                await appUserRoleService.Add(new AppUserRole
                {
                    AppRoleId = role.Id,
                    AppUserId = admin.Id
                });
            }
        } 
    }
}
