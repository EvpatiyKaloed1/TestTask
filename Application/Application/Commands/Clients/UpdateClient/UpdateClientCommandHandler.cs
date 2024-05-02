using Application.Commons.Interfaces;
using Domain.Clients;
using Domain.Clients.Dto;
using Domain.Clients.Exeptions;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using Mapster;
using MediatR;

namespace Application.Commands.Clients.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;
    private readonly IFounderRepository _founderRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepository)
    {
        _clientRepository = clientRepository;
        _founderRepository = founderRepository;
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

        if(request.Type == ClientType.LegalEntity)
        {
            var newFounder = await _founderRepository.GetFounderByIdAsync(request.Founder.Value, cancellationToken);
            var updateRequest = new UpdateClientDto(request.Inn, request.Type, request.Name, newFounder);
            client.Update(updateRequest);
        }
        else
        {
            client.Update(request.Adapt<UpdateClientDto>());    
        }

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