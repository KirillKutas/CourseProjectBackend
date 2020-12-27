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

namespace Application.Games.Commands.CRUDGames
{
    public class DeleteGameCommand: IRequest<List<Game>>
    {
        public string GameId { get; set; }
    }
    public class DeleteGameHandler : IRequestHandler<DeleteGameCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public DeleteGameHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<DeleteGameCommand, List<Game>>.Handle(DeleteGameCommand command, CancellationToken cancellationToken)
        {
            var game = _context.Games
                .Include(i => i.Categories)
                .Include(i => i.Genres)
                .Include(u => u.Users)
                .Include(c => c.Comments)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            if (game == null)
            {
                return null;
            }

            game.Categories = new List<Category>();
            game.Genres = new List<Genre>();
            game.Users = new List<User>();
            game.Comments = new List<Domain.Entities.Comment>();
            await _context.SaveChangesAsync(cancellationToken);

            var deleteGame = _context.Games.FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            _context.Games.Remove(deleteGame);

            await _context.SaveChangesAsync(cancellationToken);

            var result = _context.Games.ToList();

            return result;
        }
    }
}
