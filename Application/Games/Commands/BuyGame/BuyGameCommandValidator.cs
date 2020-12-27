using FluentValidation;

namespace Application.Games.Commands.BuyGame
{
    /// <summary>
    /// SaveCommentCommandValidator command validator
    /// <seealso cref="AbstractValidator{T}" />
    public class BuyGameCommandValidator : AbstractValidator<BuyGameCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyGameCommandValidator"/> class.
        /// </summary>
        public BuyGameCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
