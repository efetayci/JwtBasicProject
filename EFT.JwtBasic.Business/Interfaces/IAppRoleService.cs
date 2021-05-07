using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.Business.Interfaces
{
    public interface IAppRoleService : IGenericService<AppRole>
    {
        Task<AppRole> FindByNameAsync(string roleName);
    }
}
