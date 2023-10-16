using FluentValidation;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models.Validations
{
    public class UpdateValidation : AbstractValidator<UpdateCoupon>
    {
        public UpdateValidation()
        {
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }
    }
}
