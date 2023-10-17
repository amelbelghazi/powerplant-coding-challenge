using FluentValidation;
using ProductionPlanAPI.Dtos.Request;

namespace ProductionPlanAPI.Validations
{
    public class CreateProductionPlanValidator : AbstractValidator<CreateProductionPlanRequest>
    {
        public CreateProductionPlanValidator() 
        {
            this.RuleFor(x => x.Fuels).NotNull();
            this.RuleFor(x => x.Powerplants).NotNull().NotEmpty();
            this.RuleFor(x => x.Load).NotEmpty().Custom((load, context) =>
            {
                if (load <= 0)
                {
                    context.AddFailure("The load value is invalid");
                }
            });
        }
    }
}
