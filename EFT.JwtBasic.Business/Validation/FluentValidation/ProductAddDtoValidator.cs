using EFT.JwtBasic.Entites.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Validation.FluentValidation
{
    public class ProductAddDtoValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field can not be empty");
        }
    }
}
