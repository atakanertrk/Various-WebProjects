using FluentValidation;
using OnurShopSecond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond.GeneralOperations.FluentDataValidation
{
    public class ProductValidation : AbstractValidator<UrunModel>
    {
        public ProductValidation()
        {
            RuleFor(UrunModel => UrunModel.ProductName).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(UrunModel => UrunModel.Price).NotNull().NotEmpty().Must(BeValidPrice).WithMessage("Price property should be example 12.50 (dont use comma)");
            RuleFor(UrunModel => UrunModel.Properties).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(UrunModel => UrunModel.Amount).NotNull().NotEmpty().InclusiveBetween(0,10000000);
        }
        protected bool BeValidPrice(decimal price)
        {
            foreach (var item in price.ToString())
            {
                if (item == ',')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
