using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.DTOS.CategoryDTOS
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class CategoryPostValidator : AbstractValidator<CategoryPostDto>
    {

        public CategoryPostValidator()
        {
            RuleFor(x => x.Name).MaximumLength(40).WithMessage("Max length 50 ola biler")
                .NotEmpty().WithMessage("Bos ola bilmez");
            
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile!=null&&!x.ImageFile.ContentType.Contains("image/"))
                {
                    context.AddFailure("ImageFile", "Image file olmalidir");
                }
            });
        }
    }
}
