using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;

namespace Application.Common.Behaviors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ITokenService _tokenService;

        public RequestLogger(ILogger<TRequest> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Request: {Name} {@UserId} {@Request}", name, _tokenService.GetUserName(), request);

            return Task.CompletedTask;
        }
    }
}
