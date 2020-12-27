using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands.Comment
{
    public class DeleteCommentCommand : IRequest<Game>
    {
        public string GameId { get; set; }
        public string CommentId { get; set; }
    }

    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Game>
    {
        private readonly IDbContext _context;

        public DeleteCommentHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<Game> IRequestHandler<DeleteCommentCommand, Game>.Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = _context.Comments.FirstOrDefault(item => item.Id == Guid.Parse(command.CommentId));

            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);

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