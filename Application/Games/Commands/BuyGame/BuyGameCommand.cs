using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands.BuyGame
{
    public class BuyGameCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
    }
    public class BuyGameHandler : IRequestHandler<BuyGameCommand, bool>
    {
        private readonly IDbContext _context;

        public BuyGameHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<bool> IRequestHandler<BuyGameCommand, bool>.Handle(BuyGameCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Include(g => g.Games)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));
            if (user == null)
            {
                return false;
            }

            var game = _context.Games.FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            if (game == null)
                return false;

            if (user.Invoice >= game.Price)
            {
                user.Invoice = user.Invoice - game.Price;
                user.Games.Add(game);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
