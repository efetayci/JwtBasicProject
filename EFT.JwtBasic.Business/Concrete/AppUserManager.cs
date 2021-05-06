using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser> , IAppUserService
    {
        public AppUserManager(IGenericDal<AppUser> genericDal):base(genericDal)
        {

        }
    }
}
