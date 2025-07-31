using Csnp.Credential.Infrastructure.Persistence;
using Csnp.SeedWork.Application.UnitOfWork;

namespace Csnp.Credential.Infrastructure.UnitOfWork;

public class CredentialUnitOfWork : IUnitOfWork
{
    private readonly CredentialDbContext _context;

    public CredentialUnitOfWork(CredentialDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
