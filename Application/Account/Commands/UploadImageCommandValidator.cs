using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Account.Commands
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadImageCommandValidator"/> class.
        /// </summary>
        public UploadImageCommandValidator()
        {
            RuleFor(x => x.Image)
                .NotEmpty();
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
