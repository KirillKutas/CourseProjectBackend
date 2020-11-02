using FluentValidation;

namespace Application.Registration.Commands
{
    /// <summary>
    /// Login command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{AuthenticateCommand}" />
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationCommandValidator"/> class.
        /// </summary>
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}