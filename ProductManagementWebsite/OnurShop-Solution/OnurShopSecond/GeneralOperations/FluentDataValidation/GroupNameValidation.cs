using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OnurShopSecond.Models;

namespace OnurShopSecond.GeneralOperations.FluentDataValidation
{
    public class GroupNameValidation : AbstractValidator<UrunGurubuModel>
    {
        public GroupNameValidation()
        {
            RuleFor(UrunGurubuModel=>UrunGurubuModel.GroupName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
