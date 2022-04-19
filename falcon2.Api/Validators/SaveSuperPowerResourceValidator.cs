using FluentValidation;
using falcon2.Api.Resources;

namespace falcon2.Api.Validators
{
    public class SaveSuperPowerResourceValidator : AbstractValidator<SaveSuperPowerResource>
    {
        public SaveSuperPowerResourceValidator()
        {
            RuleFor(sp=>sp.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(sp => sp.SuperHeroId)
                .NotEmpty()
                .WithMessage("Super hero Id cannot be 0.");
        }

    }
}
