using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.UserApi.DTOs
{
    public class RegisterDtos
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

     public class RegisterDtoValidator:AbstractValidator<RegisterDtos>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(20).MinimumLength(4);
            RuleFor(x=>x.UserName).NotEmpty().MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(20);
            RuleFor(x=>x.ConfirmPassword).NotEmpty().MinimumLength(8).MaximumLength(20);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password!=x.Password)
                {
                    context.AddFailure("ConfirmPassword", "Password ile confirm pasword eyni deil");
                }
            });
        }
    }
}
