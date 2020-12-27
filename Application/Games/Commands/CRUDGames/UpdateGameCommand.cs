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
    public class UpdateGameCommand: IRequest<bool>
    {
        public CrudGameModel game { get; set; }
    }
    public class UpdateGameHandler : IRequestHandler<UpdateGameCommand, bool>
    {
        private readonly IDbContext _context;

        public UpdateGameHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<bool> IRequestHandler<UpdateGameCommand, bool>.Handle(UpdateGameCommand command, CancellationToken cancellationToken)
        {
            var game = _context.Games
                .Include(u => u.Users)
                .Include(c => c.Categories)
                .Include(g => g.Genres)
                .Include(c => c.Comments)
                .FirstOrDefault(item => item.Id == Guid.Parse(command.game.Id));

            game.GameName = command.game.GameName;
            game.Price = command.game.Price;
            game.ReleaseDate = command.game.ReleaseDate;
            game.Developer = command.game.Developer;
            game.Publisher = command.game.Publisher;
            game.Description = command.game.Description;
            game.Image = command.game.Image;
            game.MinSysReqOc = command.game.MinSysReqOc;
            game.MinSysReqProcessor = command.game.MinSysReqProcessor;
            game.MinSysReqRAM = command.game.MinSysReqRAM;
            game.MinSysReqVideoCard = command.game.MinSysReqVideoCard;
            game.RecSysReqOc = command.game.RecSysReqOc;
            game.RecSysReqProcessor = command.game.RecSysReqProcessor;
            game.RecSysReqRAM = command.game.RecSysReqRAM;
            game.RecSysReqVideoCard = command.game.RecSysReqVideoCard;
            game.DiskSpace = command.game.DiskSpace;

            game.Users = command.game.Users;
            game.Comments = command.game.Comments;

            var newGameGenres = new List<Genre>();
            foreach (var item in command.game.Genres)
            {
                var genre = _context.Genres.FirstOrDefault(i => i.Name == item.Name);
                newGameGenres.Add(genre);
            }

            var newGameCategories = new List<Category>();
            foreach (var item in command.game.Categories)
            {
                var category = _context.Categories.FirstOrDefault(i => i.Name == item.Name);
                if (category != null)
                {
                    newGameCategories.Add(category);
                }
            }


            game.Genres = newGameGenres;
            game.Categories = newGameCategories;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
