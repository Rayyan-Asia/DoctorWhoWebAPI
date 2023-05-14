using DoctorWhoDomain;
using FluentValidation;

namespace DoctorWho.Web
{
    public class EnemyValidator : AbstractValidator<Enemy>
    {
        public EnemyValidator()
        {
            RuleFor(author => author.EnemyName).NotNull().MaximumLength(32).NotEmpty()
                .WithMessage("Name must not be empty and less than or equal 32 characters.");

        }
    }
}
