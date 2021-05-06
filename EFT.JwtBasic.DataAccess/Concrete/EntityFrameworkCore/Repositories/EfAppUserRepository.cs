using EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Context;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        
        public Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            List<AppRole> roles = new List<AppRole>();
            
            var context = new JwtContext();
            var user = context.AppUsers.Where(x => x.UserName == userName).FirstOrDefault();
            var allRole = context.AppUserRoles.Where(x => x.AppUserId == user.Id).ToList();

            foreach (var item in allRole)
            {
                AppRole role = new AppRole()
                {
                    Id = item.Id,
                    Name = item.AppRole.Name
                };
                roles.Add(role);
            }
            return roles;
        }
    }
}
