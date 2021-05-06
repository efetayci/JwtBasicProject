using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Concrete
{
    public class AppUserRoleManager : GenericManager<AppUserRole> ,IAppUserRoleService
    {
        public AppUserRoleManager(IGenericDal<AppUserRole> genericDal):base(genericDal)
        {

        }
    }
}
