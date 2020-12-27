using FluentValidation;

namespace Application.Games.Commands.CheckGame
{
    /// <summary>
    /// SaveCommentCommandValidator command validator
    /// <seealso cref="AbstractValidator{T}" />
    public class CheckGameCommandValidator : AbstractValidator<CheckGameCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckGameCommandValidator"/> class.
        /// </summary>
        public CheckGameCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}