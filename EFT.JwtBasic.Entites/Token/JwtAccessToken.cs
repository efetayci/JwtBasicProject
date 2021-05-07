using EFT.JwtBasic.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Entites.Token
{
    public class JwtAccessToken : IToken
    {
        public string Token { get; set; }
    }
}
