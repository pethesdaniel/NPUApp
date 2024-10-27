using FluentValidation;
using NPUApp.BLL.Model.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Validation.Model.RequestDTOs
{
    public class CreateOrEditPostDtoValidator : AbstractValidator<CreateOrEditPostDto>
    {
        public CreateOrEditPostDtoValidator()
        {
            RuleFor(x => x.PictureUrl).NotEmpty();
            RuleFor(x => x.Title).MinimumLength(3);
            RuleFor(x => x.Parts).NotEmpty();
        }
    }
}
