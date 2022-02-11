using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Apps.DTOS.ProductDTOS
{
    public class ProductPostDtos 
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
    }


    public class ProductAbsrtactValidator:AbstractValidator<ProductPostDtos>
    {

        public ProductAbsrtactValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("50den uzun ola bilmez").NotEmpty().WithMessage("bos ola bilmez");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("categoruya nul ola bilmez");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.SalePrice < x.CostPrice)

                    context.AddFailure("SalePrice", "SalePrice CostPrice den asagi ola bilmez");
            });

        }
    
    }
}
