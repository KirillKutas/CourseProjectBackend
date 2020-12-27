using System;
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
    public class GetUserGamesCommand : IRequest<List<Game>>
    {
        public string Id { get; set; }
    }

    public class GetUserGamesHandler : IRequestHandler<GetUserGamesCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public GetUserGamesHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<GetUserGamesCommand, List<Game>>.Handle(GetUserGamesCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Include(g => g.Games).ThenInclude(c => c.Categories)
                .Include(g => g.Games).ThenInclude(c => c.Genres)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.Id));

            if (user != null)
            {
                return user.Games;
            }

            return null;
        }
    }
}