using FluentValidation;

namespace Application.Games.Commands.Comment
{
    /// <summary>
    /// SaveCommentCommandValidator command validator
    /// <seealso cref="AbstractValidator{T}" />
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentCommandValidator"/> class.
        /// </summary>
        public DeleteCommentCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty();
            RuleFor(x => x.CommentId)
                .NotEmpty();
        }
    }
}