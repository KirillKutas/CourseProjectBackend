using FluentValidation;

namespace Application.Games.Commands.Comment
{
    /// <summary>
    /// SaveCommentCommandValidator command validator
    /// <seealso cref="AbstractValidator{T}" />
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommentCommandValidator"/> class.
        /// </summary>
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
            RuleFor(x => x.CommentId)
                .NotEmpty();
            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}