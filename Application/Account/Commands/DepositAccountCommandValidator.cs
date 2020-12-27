using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Account.Commands
{
    public class DepositAccountCommandValidator : AbstractValidator<DepositAccountCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepositAccountCommandValidator"/> class.
        /// </summary>
        public DepositAccountCommandValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
