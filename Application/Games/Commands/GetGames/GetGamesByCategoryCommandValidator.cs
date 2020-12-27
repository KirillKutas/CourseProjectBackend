using FluentValidation;

namespace Application.Games.Commands
{
    /// <summary>
    /// GetGamesByCategoryCommand command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class GetGamesByCategoryCommandValidator : AbstractValidator<GetGamesByCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGamesByCategoryCommandValidator"/> class.
        /// </summary>
        public GetGamesByCategoryCommandValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty();
        }
    }
}
