﻿using EFT.JwtBasic.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Interfaces
{
    public interface IJwtService
    {
         string GenerateJwtToken(AppUser appUser, List<AppRole> roles)
    }
}