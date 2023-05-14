using DoctorWhoDomain;
using FluentValidation;

namespace DoctorWho.Web
{
    public class CompanionValidator : AbstractValidator<Companion>
    {
        public CompanionValidator()
        {
            RuleFor(companion => companion.CompanionName).NotNull().MaximumLength(64).NotEmpty()
                .WithMessage("Name must not be empty and less than or equal 64 characters."); 
            RuleFor(companion => companion.WhoPlayed).NotNull().MaximumLength(64).NotEmpty()
                .WithMessage("Who played must not be empty and less than or equal 64 characters.");

        }
    }

}
