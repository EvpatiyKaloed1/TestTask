using Domain.Common.ValueObjects;
using Domain.Founders;

namespace Application.Commons.Interfaces;

public interface IFounderRepository
{
    Task<Founder> CreateFounderAsync(Founder founder, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken token);

    Task<IEnumerable<Founder>> GetAllAsync(CancellationToken token);

    Task<Founder> GetFounderByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Founder> GetFounderByInnAsync(Inn inn, CancellationToken cancellationToken);

    Task<IEnumerable<Founder>> GetFoundersAsync(IEnumerable<Guid> founders, CancellationToken cancellationToken);

    Task UpdateAsync(Founder founder, CancellationToken cancellationToken);
}