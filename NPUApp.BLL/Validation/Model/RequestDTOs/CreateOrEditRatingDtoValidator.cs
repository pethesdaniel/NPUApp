using FluentValidation;
using NPUApp.BLL.Model.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Validation.Model.RequestDTOs
{
    public class CreateOrEditRatingDtoValidator : AbstractValidator<CreateOrEditRatingDto>
    {
        public CreateOrEditRatingDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.CreativityScore).GreaterThan(0);
            RuleFor(x => x.UniquenessScore).GreaterThan(0);
        }
    }
}
