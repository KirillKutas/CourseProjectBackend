using FluentValidation;

namespace Application.Games.Commands
{
    /// <summary>
    /// SaveCommentCommandValidator command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class SaveCommentCommandValidator : AbstractValidator<SaveCommentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCommentCommandValidator"/> class.
        /// </summary>
        public SaveCommentCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
            RuleFor(x => x.GameId)
                .NotEmpty();
            RuleFor(x => x.Comment)
                .NotEmpty();
        }
    }
}