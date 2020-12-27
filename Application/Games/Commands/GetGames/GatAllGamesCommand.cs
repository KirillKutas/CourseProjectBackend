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

namespace Application.Games.Commands.GetGames
{
    public class GatAllGamesCommand : IRequest<List<Game>>
    {
    }
    public class GatAllGamesHandler : IRequestHandler<GatAllGamesCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public GatAllGamesHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<GatAllGamesCommand, List<Game>>.Handle(GatAllGamesCommand command,
            CancellationToken cancellationToken)
        {

            return _context.Games.ToList();

        }
    }
}
