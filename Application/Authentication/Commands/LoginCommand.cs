using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using System;
using Domain.Entities;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.Authentication.Commands
{
    public class LoginCommand : IRequest<User>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly IDbContext _context;
        private readonly IHashService _hashService;

        public LoginCommandHandler(IDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        async Task<User> IRequestHandler<LoginCommand, User>.Handle(LoginCommand command, CancellationToken cancellationToken)
        {

            var user = _context.Users.FirstOrDefault(item => item.UserName == command.UserName);

            if (user == null || !_hashService.Verify(user.PasswordSalt, user.PasswordHash, command.Password))
            {
                throw new Exception("Invalid user name or password");
            }

            return user;
        }
    }
}