using FluentValidation;

namespace Application.Games.Commands.GetGames
{
    /// <summary>
    /// GetGamesByCategoryCommand command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class GetGamesByGenreCommandValidator : AbstractValidator<GetGamesByGenreCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGamesByGenreCommandValidator"/> class.
        /// </summary>
        public GetGamesByGenreCommandValidator()
        {
            RuleFor(x => x.Genre)
                .NotEmpty();
        }
    }
}
