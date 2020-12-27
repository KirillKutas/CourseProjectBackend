using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands.GetGames
{
    public class SearchGameCommand : IRequest<List<Game>>
    {
        public string SearchString { get; set; }
    }
    public class SearchGameHandler : IRequestHandler<SearchGameCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public SearchGameHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<SearchGameCommand, List<Game>>.Handle(SearchGameCommand command, CancellationToken cancellationToken)
        {
            var games = _context.Games.Where(item => EF.Functions.Like(item.GameName, $"%{command.SearchString}%"))
                .ToList();

            return games;
        }
    }
}
