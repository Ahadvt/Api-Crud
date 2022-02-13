using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.UserApi.DTOs
{
    public class LoginDtos
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtosValidator:AbstractValidator<LoginDtos>
    {
        public LoginDtosValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(4).MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4).MaximumLength(20);
        }
    }
}
