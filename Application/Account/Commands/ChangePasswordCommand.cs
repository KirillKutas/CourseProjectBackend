using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands
{
    public class ChangePasswordCommand: IRequest<bool>
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IDbContext _context;
        private readonly IHashService _hashService;

        public ChangePasswordHandler(IDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        async Task<bool> IRequestHandler<ChangePasswordCommand, bool>.Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));

            if (user == null)
                return false;

            if (!_hashService.Verify(user.PasswordSalt, user.PasswordHash, command.OldPassword))
                return false;

            _hashService.CreatePasswordHash(command.NewPassword, out var passwordSalt, out var passwordHash);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
