using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands.CheckGame
{
    public class CheckGameCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
    }

    public class CheckGameHandler : IRequestHandler<CheckGameCommand, bool>
    {
        private readonly IDbContext _context;

        public CheckGameHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<bool> IRequestHandler<CheckGameCommand, bool>.Handle(CheckGameCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Include(g => g.Games)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));
            if (user == null)
            {
                return false;
            }

            var game = user.Games.FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));
            if (game != null)
            {
                return true;
            }

            return false;
        }
    }
}
