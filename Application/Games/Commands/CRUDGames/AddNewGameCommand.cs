using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Games.Commands.CRUDGames
{
    public class AddNewGameCommand: IRequest<Game>
    {
        public CrudGameModel game { get; set; }
    }
    public class AddNewGameGandler : IRequestHandler<AddNewGameCommand, Game>
    {
        private readonly IDbContext _context;

        public AddNewGameGandler(IDbContext context)
        {
            _context = context;
        }

        async Task<Game> IRequestHandler<AddNewGameCommand, Game>.Handle(AddNewGameCommand command, CancellationToken cancellationToken)
        {
            var newGame = new Game();
            newGame.Id = Guid.NewGuid();
            newGame.GameName = command.game.GameName;
            newGame.Price = command.game.Price;
            newGame.ReleaseDate = command.game.ReleaseDate;
            newGame.Developer = command.game.Developer;
            newGame.Publisher = command.game.Publisher;
            newGame.Description = command.game.Description;
            newGame.Image = command.game.Image;
            newGame.MinSysReqOc = command.game.MinSysReqOc;
            newGame.MinSysReqProcessor = command.game.MinSysReqProcessor;
            newGame.MinSysReqRAM = command.game.MinSysReqRAM;
            newGame.MinSysReqVideoCard = command.game.MinSysReqVideoCard;
            newGame.RecSysReqOc = command.game.RecSysReqOc;
            newGame.RecSysReqProcessor = command.game.RecSysReqProcessor;
            newGame.RecSysReqRAM = command.game.RecSysReqRAM;
            newGame.RecSysReqVideoCard = command.game.RecSysReqVideoCard;
            newGame.DiskSpace = command.game.DiskSpace;

            newGame.Users = command.game.Users;
            newGame.Comments = command.game.Comments;

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
            

            newGame.Genres = newGameGenres;
            newGame.Categories = newGameCategories;
            

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync(cancellationToken);
            return newGame;
        }
    }
}
