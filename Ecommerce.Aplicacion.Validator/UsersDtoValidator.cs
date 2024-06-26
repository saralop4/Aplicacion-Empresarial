using Ecommerce.Aplicacion.DTO;
using FluentValidation;
using System;

namespace Ecommerce.Aplicacion.Validator
{
    public class UsersDtoValidator : AbstractValidator<UsersDto>
    {

        public UsersDtoValidator()
        {
            RuleFor(u=> u.UserName).NotEmpty().NotNull();
            RuleFor(u => u.Password).NotEmpty().NotNull();
        }

    }
}
