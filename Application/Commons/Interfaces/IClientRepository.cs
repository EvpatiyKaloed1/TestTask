using Domain.Clients;
using Domain.Common.ValueObjects;

namespace Application.Commons.Interfaces;

public interface IClientRepository
{
    Task CreateClientAsync(Client client, CancellationToken cancellationToken);

    Task DeleteClientAsync(Guid id, CancellationToken token);

    Task<IEnumerable<Client>> GetAllAsync(CancellationToken token);

    Task<Client> GetClientByIdAsync(Guid id, CancellationToken token);

    Task<Client> GetClientByInnAsync(Inn inn, CancellationToken token);

    Task UpdateAsync(Client client, CancellationToken token);
}