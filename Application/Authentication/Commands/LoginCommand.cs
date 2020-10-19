using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.Authentication.Commands
{
    public class LoginCommand : IRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class LoginCommandHandler : AsyncRequestHandler<LoginCommand>
    {
        
        protected override async Task Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // TODO: check for login do something if needed
            var credentialsIsValid = await Task.FromResult(command.UserName != null && command.Password != null);

            if (!credentialsIsValid)
            {
                throw new InvalidCredentialException();
            }
        }
    }
}