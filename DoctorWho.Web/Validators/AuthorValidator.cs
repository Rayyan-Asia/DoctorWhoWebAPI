using DoctorWhoDomain;
using FluentValidation;

namespace DoctorWho.Web
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(author => author.AuthorName).NotNull().MaximumLength(64)
                .WithMessage("Name must not be empty and less than or equal 64 characters.");
        }
    }
}
