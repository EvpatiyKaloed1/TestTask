using Application.Commons.Interfaces;
using Domain.Clients;
using Domain.Common.ValueObjects;
using Infrastructure.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Clients;

public class ClientRepository : IClientRepository
{
    private readonly Database _clientDatabase;

    public ClientRepository(Database clientDatabase)
    {
        _clientDatabase = clientDatabase;
    }

    public async Task CreateClientAsync(Client client, CancellationToken cancellationToken)
    {
        var clientDto = client.Adapt<ClientDto>();

        await _clientDatabase.Founders.Where(x => clientDto.Founders.Select(x=>x.Id).Contains(x.Id)).ExecuteDeleteAsync(cancellationToken: cancellationToken);

        await _clientDatabase.Founders.AddRangeAsync(clientDto.Founders, cancellationToken);
        await _clientDatabase.Clients.AddAsync(clientDto, cancellationToken);

        await _clientDatabase.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteClientAsync(Guid id, CancellationToken token)
    {
        await _clientDatabase.Clients.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken: token);
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken token)
    {
        var dbClients = await _clientDatabase.Clients.ToListAsync(token);

        return dbClients.Adapt<IEnumerable<Client>>();
    }

    public async Task<Client> GetClientByIdAsync(Guid id, CancellationToken token)
    {
        var dbClient = await _clientDatabase.Clients
        .Include(x => x.Founders)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: token);

        return dbClient.Adapt<Client>();
    }

    public async Task<Client> GetClientByInnAsync(Inn inn, CancellationToken token)
    {
        var dbClient = await _clientDatabase.Clients
        .Include(x => x.Founders)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Inn == inn.InnValue, cancellationToken: token);

        return dbClient.Adapt<Client>();
    }

    public async Task UpdateAsync(Client client, CancellationToken token)
    {
        var dbClient = client.Adapt<ClientDto>();

        if(dbClient.Founders == null)
        {
            await _clientDatabase.Founders
                .Where(x => x.ClientId == dbClient.Id)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.ClientId, y => null), cancellationToken: token);
        }

        _clientDatabase.Clients.Update(dbClient);
        await _clientDatabase.SaveChangesAsync(token);
    }
}