using FluentValidation;

namespace Application.Authentication.Commands
{
    /// <summary>
    /// Login command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{AuthenticateCommand}" />
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommandValidator"/> class.
        /// </summary>
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}