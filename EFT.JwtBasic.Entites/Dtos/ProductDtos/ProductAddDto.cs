using EFT.JwtBasic.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Entites.Dtos.ProductDtos
{
    public class ProductAddDto : IDto
    {
        public string Name { get; set; }
    }
}
