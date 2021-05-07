using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Validation.FluentValidation
{
    public class AppUserAddDtoValidator : AbstractValidator<AppUserAddDto>
    {
        public AppUserAddDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Username can not be empty");
        }
    }
}
