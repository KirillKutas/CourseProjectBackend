using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
