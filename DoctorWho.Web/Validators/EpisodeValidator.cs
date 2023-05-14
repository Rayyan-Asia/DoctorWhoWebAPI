using DoctorWhoDomain;
using FluentValidation;

namespace DoctorWho.Web
{
    public class EpisodeValidator : AbstractValidator<Episode>
    {
        public EpisodeValidator()
        {
            RuleFor(episode => episode.SeriesNumber).NotNull()
                .WithMessage("Series number must not be empty");
            RuleFor(episode => episode.EpisodeNumber).NotNull()
                .WithMessage("Episode number must not be empty.");
            RuleFor(episode => episode.EpisodeType).NotNull().MaximumLength(32)
                .WithMessage("Episode genres must not be empty and less than or equal 32 characters.");
            RuleFor(episode => episode.Title).NotNull().MaximumLength(100)
                .WithMessage("Episode title must not be empty and less than or equal 100 characters.");
            RuleFor(episode => episode.EpisodeDate).NotNull()
                .WithMessage("Episode date must not be empty.");
            RuleFor(episode => episode.AuthorId).NotNull()
                .WithMessage("Author Id must not be empty.");
        }
    }
}
