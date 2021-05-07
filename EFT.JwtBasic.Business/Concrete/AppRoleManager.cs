using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.Business.Concrete
{
    public class AppRoleManager : GenericManager<AppRole>, IAppRoleService
    {
        private readonly IGenericDal<AppRole> genericDal;
        public AppRoleManager(IGenericDal<AppRole> genericDal) : base(genericDal)
        {
            this.genericDal = genericDal;
        }

        public async Task<AppRole> FindByNameAsync(string roleName)
        {
            return await this.genericDal.GetByFiter(x => x.Name == roleName);
        }
    }
}
