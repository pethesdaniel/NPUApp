using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.BLL.Validation.Model.RequestDTOs;

namespace NPUApp.BLL.Validation
{
    public static class WireupValidatorsExtensions
    {
        public static void AddBllValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IValidator<CreateOrEditPostDto>, CreateOrEditPostDtoValidator>();
            serviceCollection.AddScoped<IValidator<CreateOrEditRatingDto>, CreateOrEditRatingDtoValidator>();
        }
    }
}
