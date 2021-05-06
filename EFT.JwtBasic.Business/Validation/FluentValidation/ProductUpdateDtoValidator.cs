using EFT.JwtBasic.Entites.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Validation.FluentValidation
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(x => x.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Id can not negative ");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Name must contain a maximum of 30 characters.");

        }
    }
}
