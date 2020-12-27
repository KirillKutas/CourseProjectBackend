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

namespace Application.Account.Commands
{
    public class UploadImageCommand : IRequest<string>
    {
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
    }

    public class UploadImageHandler : IRequestHandler<UploadImageCommand, string>
    {
        private readonly IDbContext _context;
        private readonly IHashService _hashService;

        public UploadImageHandler(IDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        async Task<string> IRequestHandler<UploadImageCommand, string>.Handle(UploadImageCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));

            _hashService.CreatePasswordHash(command.Image.FileName, out var passwordSalt, out var passwordHash);

            var fileName = BitConverter.ToString(passwordHash) + "_" + command.Image.FileName;
            var path = @"E:\Labs\ASP.NET Course Project\front\front-end\src\assets\Images\Account\";
            using (var fileStream = new FileStream(path + fileName, FileMode.Create))
            {
                await command.Image.CopyToAsync(fileStream);
            }

            if (user != null)
            {
                var deleteFileName = user.Image.Split('/');
                var deleteFile = new FileInfo(path + deleteFileName[deleteFileName.Length - 1]);

                if (deleteFile.Exists)
                {
                    deleteFile.Delete();
                }

                user.Image = "./assets/Images/Account/" + fileName;

                await _context.SaveChangesAsync(cancellationToken);

                return user.Image;
            }

            throw new Exception();
        }
    }
}
