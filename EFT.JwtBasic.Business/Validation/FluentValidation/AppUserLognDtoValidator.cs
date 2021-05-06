using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFT.JwtBasic.Business.Validation.FluentValidation
{
    public class AppUserLognDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLognDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name can not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty");
        }
    }
}
