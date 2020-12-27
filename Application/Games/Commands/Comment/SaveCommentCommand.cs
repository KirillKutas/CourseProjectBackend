using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands
{
    public class SaveCommentCommand : IRequest<Game>
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string Comment { get; set; }
    }

    public class SaveCommentHandler : IRequestHandler<SaveCommentCommand, Game>
    {
        private readonly IDbContext _context;

        public SaveCommentHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<Game> IRequestHandler<SaveCommentCommand, Game>.Handle(SaveCommentCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));

            var game = _context.Games.FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            var comment = new Domain.Entities.Comment()
            {
                Id = new Guid(),
                Text = command.Comment,
                User = user,
                Game = game,
                PublicationDate = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);

            var result = _context.Games
                .Include(c => c.Comments).ThenInclude(u => u.User)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            if (result == null)
            {
                return null;
            }

            var sortedComments = from c in result.Comments
                orderby c.PublicationDate descending
                select c;

            result.Comments = sortedComments.ToList();

            return result;
        }
    }
}
