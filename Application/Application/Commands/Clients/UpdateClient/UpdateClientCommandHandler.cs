using Application.Commons.Interfaces;
using Domain.Clients;
using Domain.Clients.Dto;
using Domain.Clients.Exeptions;
using Domain.Common.ValueObjects;
using Mapster;
using MediatR;

namespace Application.Commands.Clients.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        if (request.Inn != null)
        {
            await CheckInnAsync(request.Inn.Value, cancellationToken);
        }

        var client = await _clientRepository.GetClientByIdAsync(request.Id, cancellationToken);

        if (client == null)
        {
            throw new NotFoundException(request.Id);
        }

        client.Update(request.Adapt<UpdateClientDto>());

        await _clientRepository.UpdateAsync(client, cancellationToken);

        return client;
    }

    private async Task CheckInnAsync(Inn inn, CancellationToken cancellationToken)
    {
        var takenInn = await _clientRepository.GetClientByInnAsync(inn, cancellationToken);

        if (takenInn != null)
        {
            throw new InnTakenException(inn);
        }
    }
}