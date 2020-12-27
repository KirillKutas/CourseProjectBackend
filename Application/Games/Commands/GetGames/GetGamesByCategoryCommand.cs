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
    public class GetGamesByCategoryCommand : IRequest<List<Game>>
    {
        public string Category { get; set; }
        public int? Count { get; set; }
    }

    public class GetGamesByCategoryHandler : IRequestHandler<GetGamesByCategoryCommand, List<Game>>
    {
        private readonly IDbContext _context;

        public GetGamesByCategoryHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<List<Game>> IRequestHandler<GetGamesByCategoryCommand, List<Game>>.Handle(GetGamesByCategoryCommand command, CancellationToken cancellationToken)
        {
            if (command.Category == "TopSellers")
            {
                var data = _context.Games
                    .ToList();

                var sortedData = from d in data
                    orderby d.CountOfBuy descending
                    select d;

                if (command.Count != null)
                {
                    return sortedData.Take((int) command.Count).ToList();
                }

                return sortedData.ToList();
            }

            if (command.Category == "NewReleases")
            {
                var data = _context.Games
                    .ToList();

                var sortedData = from d in data
                    orderby d.ReleaseDate descending
                    select d;

                if (command.Count != null)
                {
                    return sortedData.Take((int)command.Count).ToList();
                }

                return sortedData.ToList();
            }

            if (command.Category == "VirtualReality")
            {
                var data = _context.Games
                    .Include(c => c.Categories)
                    .ToList();

                var selectedData = from d in data
                    where d.Categories.FirstOrDefault(c => c.Name == "Virtual reality") == null ? false : true
                    select d;

                if (command.Count != null)
                {
                    return selectedData.Take((int)command.Count).ToList();
                }

                return selectedData.ToList();
            }
            else
            {
                var data = _context.Games
                    .Include(c => c.Categories)
                    .ToList();

                var selectedData = from d in data
                    where d.Categories.FirstOrDefault(c => c.Name == command.Category) == null ? false : true
                    select d;

                if (command.Count != null)
                {
                    return selectedData.Take((int)command.Count).ToList();
                }

                return selectedData.ToList();
            }
        }
    }
}
