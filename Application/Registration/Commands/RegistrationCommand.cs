using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Registration.Commands
{
    public class RegistrationCommand : IRequest<User>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, User>
    {
        private readonly IDbContext _context;
        private readonly IHashService _hashService;

        public RegistrationCommandHandler(IDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        async Task<User> IRequestHandler<RegistrationCommand, User>.Handle(RegistrationCommand command, CancellationToken cancellationToken)
        {

            var user = _context.Users.FirstOrDefault(item => item.UserName == command.UserName);

            if (user != null)
            {
                throw new Exception("Such user already exists");
            }
            _hashService.CreatePasswordHash(command.Password, out var passwordSalt, out var passwordHash);
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = command.UserName,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Role = Role.User,
                Image = "./assets/Images/NewUser.jpg",
                Invoice = 100
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            
            return newUser;
        }
    }
}
