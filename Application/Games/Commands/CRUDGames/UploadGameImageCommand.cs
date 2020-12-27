using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Games.Commands.CRUDGames
{
    public class UploadGameImageCommand : IRequest<bool>
    {
        public IFormFile Image { get; set; }
        public string GameId { get; set; }
    }
    public class UploadGameImageHandler : IRequestHandler<UploadGameImageCommand, bool>
    {
        private readonly IDbContext _context;
        private readonly IHashService _hashService;

        public UploadGameImageHandler(IDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        async Task<bool> IRequestHandler<UploadGameImageCommand, bool>.Handle(UploadGameImageCommand command, CancellationToken cancellationToken)
        {
            var game = _context.Games.FirstOrDefault(item => item.Id == Guid.Parse(command.GameId));

            _hashService.CreatePasswordHash(command.Image.FileName, out var passwordSalt, out var passwordHash);

            var fileName = BitConverter.ToString(passwordHash) + "_" + command.Image.FileName;
            var path = @"E:\Labs\ASP.NET Course Project\front\front-end\src\assets\Images\Games\";
            using (var fileStream = new FileStream(path + fileName, FileMode.Create))
            {
                await command.Image.CopyToAsync(fileStream);
            }

            if (game != null)
            {
                game.Image = "./assets/Images/Games/" + fileName;

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            throw new Exception();
        }
    }
}
