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
    public class UpdateCommentCommand : IRequest<Game>
    {
        public string GameId { get; set; }
        public string CommentId { get; set; }
        public string Text { get; set; }
    }

    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, Game>
    {
        private readonly IDbContext _context;

        public UpdateCommentHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<Game> IRequestHandler<UpdateCommentCommand, Game>.Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = _context.Comments.FirstOrDefault(item => item.Id == Guid.Parse(command.CommentId));

            if (comment == null)
            {
                return null;
            }

            comment.Text = command.Text;

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
