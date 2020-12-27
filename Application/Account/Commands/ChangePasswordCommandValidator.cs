using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Account.Commands
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordCommandValidator"/> class.
        /// </summary>
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty();
            RuleFor(x => x.OldPassword)
                .NotEmpty();
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
