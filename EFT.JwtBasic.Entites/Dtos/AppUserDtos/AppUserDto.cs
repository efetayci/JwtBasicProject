using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Entites.Dtos.AppUserDtos
{
    public class AppUserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
