using Application.Commons.Interfaces;
using Domain.Common.ValueObjects;
using Infrastructure.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly Database _clientDatabase;

    public ClientRepository(Database clientDatabase)
    {
        _clientDatabase = clientDatabase;
    }

    public async Task CreateClientAsync(Domain.Clients.Client client, CancellationToken cancellationToken)
    {
        var clientDto = client.Adapt<ClientDto>();
        await _clientDatabase.Clients.AddAsync(clientDto, cancellationToken);
        await _clientDatabase.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteClientAsync(Guid id, CancellationToken token)
    {
        await _clientDatabase.Clients.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken: token);
    }

    public async Task<IEnumerable<Domain.Clients.Client>> GetAllAsync(CancellationToken token)
    {
        var dbClients = await _clientDatabase.Clients.ToListAsync(token);

        return dbClients.Adapt<IEnumerable<Domain.Clients.Client>>();
    }

    public async Task<Domain.Clients.Client> GetClientByIdAsync(Guid id, CancellationToken token)
    {
        var dbClient = await _clientDatabase.Clients
        .Include(x => x.Founders)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: token);

        return dbClient.Adapt<Domain.Clients.Client>();
    }

    public async Task<Domain.Clients.Client> GetClientByInnAsync(Inn inn, CancellationToken token)
    {
        var dbClient = await _clientDatabase.Clients
        .Include(x => x.Founders)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Inn == inn.InnValue, cancellationToken: token);

        return dbClient.Adapt<Domain.Clients.Client>();
    }

    public async Task UpdateAsync(Domain.Clients.Client client, CancellationToken token)
    {
        var dbClient = client.Adapt<ClientDto>();
        _clientDatabase.Clients.Update(dbClient);
        await _clientDatabase.SaveChangesAsync(token);
    }
}