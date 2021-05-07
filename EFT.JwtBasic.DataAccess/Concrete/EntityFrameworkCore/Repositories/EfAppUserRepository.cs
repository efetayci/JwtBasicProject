using EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Context;
using EFT.JwtBasic.DataAccess.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.JwtBasic.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            using var context = new JwtContext();
            return await context.AppUsers.Join(context.AppUserRoles, u => u.Id, ur => ur.AppUserId,
                (user, userRole) => new
                {
                    user = user,
                    userRole = userRole
                }).Join(context.AppRoles, two => two.userRole.AppRoleId, r => r.Id, (twotable, role) => new
                {
                    user = twotable.user,
                    userRole = twotable.userRole,
                    role = role
                }).Where(x => x.user.UserName == userName).Select(x => new AppRole
                {
                    Id = x.role.Id,
                    Name = x.role.Name
                }).ToListAsync();
        }
    }
}
