using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Account.Commands
{
    public class DepositAccountCommand: IRequest<int>
    {
        public string UserId { get; set; }
        public int Amount { get; set; }
    }

    public class DepositAccountHandler : IRequestHandler<DepositAccountCommand, int>
    {
        private readonly IDbContext _context;

        public DepositAccountHandler(IDbContext context)
        {
            _context = context;
        }

        async Task<int> IRequestHandler<DepositAccountCommand, int>.Handle(DepositAccountCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(item => item.Id == Guid.Parse(command.UserId));

            if (user != null)
            {
                user.Invoice += command.Amount;

                await _context.SaveChangesAsync(cancellationToken);
            }

            return user.Invoice;
        }
    }
}
