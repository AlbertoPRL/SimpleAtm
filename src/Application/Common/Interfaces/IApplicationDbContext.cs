using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
