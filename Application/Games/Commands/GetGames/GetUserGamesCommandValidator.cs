using FluentValidation;

namespace Application.Games.Commands.GetGames
{
    /// <summary>
    /// GetGamesByCategoryCommand command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class GetUserGamesCommandValidator : AbstractValidator<GetUserGamesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserGamesCommandValidator"/> class.
        /// </summary>
        public GetUserGamesCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}