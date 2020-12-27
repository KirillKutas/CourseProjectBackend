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
    public class GetGamesByGenreCommand : IRequest<List<Game>>
    {
        public string Genre { get; set; }
    }

    public class GetGamesByGenreHandler : IRequestHandler<GetGamesByGenreCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public GetGamesByGenreHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<GetGamesByGenreCommand, List<Game>>.Handle(
            GetGamesByGenreCommand command, CancellationToken cancellationToken)
        {
            if (command.Genre == "FeeToPlay")
            {
                command.Genre = "Free to play";
            }

            var data = _context.Games
                .Include(g => g.Genres)
                .ToList();

            var selectedData = from d in data
                where d.Genres.FirstOrDefault(c => c.Name == command.Genre) == null ? false : true
                orderby d.CountOfBuy descending
                select d;

            return selectedData.ToList();
        }
    }
}
