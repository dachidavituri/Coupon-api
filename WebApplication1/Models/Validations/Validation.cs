using FluentValidation;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models.Validations
{
    public class Validation : AbstractValidator<CouponDTO>
    {
        public Validation() {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }
    }
}
