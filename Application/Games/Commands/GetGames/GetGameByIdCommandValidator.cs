using FluentValidation;

namespace Application.Games.Commands
{
    /// <summary>
    /// GetGamesByCategoryCommand command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class GetGamesByIdCommandValidator : AbstractValidator<GetGameByIdCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGamesByIdCommandValidator"/> class.
        /// </summary>
        public GetGamesByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}