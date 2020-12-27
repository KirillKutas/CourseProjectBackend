using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Games.Commands
{
    public class GetGameByIdCommand : IRequest<Game>
    {
        public string Id { get; set; }
    }

    public class GetGameByIdHandler : IRequestHandler<GetGameByIdCommand, Game>
    {
        private readonly IDbContext _context;

        public GetGameByIdHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<Game> IRequestHandler<GetGameByIdCommand, Game>.Handle(GetGameByIdCommand command, CancellationToken cancellationToken)
        {
            var result = _context.Games
                .Include(c => c.Comments).ThenInclude(u => u.User)
                .Include(g => g.Genres)
                .Include(c => c.Categories)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.Id));

            var sortedComments = from c in result.Comments
                orderby c.PublicationDate descending
                select c;

            result.Comments = sortedComments.ToList();

            return result;
        }
    }
}
