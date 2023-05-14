using DoctorWhoDomain;
using FluentValidation;

namespace DoctorWho.Web
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            RuleFor(d => d.DoctorName).MaximumLength(64).NotNull()
                .WithMessage("Name must not be empty and less than or equal 64 characters.");
            RuleFor(d => d.FirstEpisodeDate).LessThanOrEqualTo(d => d.LastEpisodeDate)
                .When(d => d.LastEpisodeDate != null)
                .WithMessage("First Episode Date can't be after last episode date");
            RuleFor(d => d.DoctorNumber).NotNull()
            .WithMessage("Doctor number must not be empty.");
            RuleFor(d => d.BirthDate).NotNull()
                .WithMessage("Birthdate must not be empty.");
            RuleFor(d => d.BirthDate).LessThan(d => d.FirstEpisodeDate)
                .When(d => d.FirstEpisodeDate != null)
                .WithMessage("Birthdate must be before first episode date.");
        }
    }
}
