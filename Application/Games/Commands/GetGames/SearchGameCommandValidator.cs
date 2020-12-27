using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Games.Commands.GetGames
{
    /// <summary>
    /// GetGamesByCategoryCommand command validator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class SearchGameCommandValidator : AbstractValidator<SearchGameCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGameCommandValidator"/> class.
        /// </summary>
        public SearchGameCommandValidator()
        {
            RuleFor(x => x.SearchString)
                .NotEmpty();
        }
    }
}
