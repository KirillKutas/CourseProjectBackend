using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands
{
    public class GetRecommendedGamesCommand : IRequest<List<Game>>
    {
    }
    public class GetRecommendedGamesHandler : IRequestHandler<GetRecommendedGamesCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public GetRecommendedGamesHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<GetRecommendedGamesCommand, List<Game>>.Handle(GetRecommendedGamesCommand command, CancellationToken cancellationToken)
        {
            var result = new List<Game>();

            var games = _context.Games
                .Include(u => u.Users)
                .Include(c => c.Categories)
                .Include(g => g.Genres)
                .ToList();

            var sortedByReleaseDate = from d in games
                orderby d.ReleaseDate descending
                select d;

            var sortedByTopSellers = from d in games
                orderby d.CountOfBuy descending
                select d;

            foreach (var item in sortedByReleaseDate)
            {
                if (games.Count < 6)
                {
                    item.Users = new List<User>();
                    result.Add(item);
                }
                else
                {
                    break;
                }
            }

            foreach (var item in sortedByTopSellers)
            {
                if (games.Count < 11)
                {
                    if (!result.Exists(g => g.Id == item.Id))
                    {
                        item.Users = new List<User>();
                        result.Add(item);
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
