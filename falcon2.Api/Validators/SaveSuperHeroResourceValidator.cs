using FluentValidation;
using falcon2.Api.Resources;

namespace falcon2.Api.Validators
{
    public class SaveSuperHeroResourceValidator : AbstractValidator<SaveSuperHeroResource>
    {
        public SaveSuperHeroResourceValidator()
        {
            RuleFor(sh => sh.Name)
                .NotEmpty()
                .MaximumLength(50);

        }
    }
}
