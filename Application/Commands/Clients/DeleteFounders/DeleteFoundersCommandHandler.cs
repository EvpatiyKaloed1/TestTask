using Application.Commons.Interfaces;
using Domain.Clients;
using MediatR;

namespace Application.Commands.Clients.DeleteFounders;

public class DeleteFoundersCommandHandler : IRequestHandler<DeleteFoundersCommand, Client>
{
    private readonly IClientRepository _clientRepository;
    private readonly IFounderRepository _founderRepository;

    public DeleteFoundersCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepository)
    {
        _clientRepository = clientRepository;
        _founderRepository = founderRepository;
    }

    public async Task<Client> Handle(DeleteFoundersCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
        var founders = await _founderRepository.GetFoundersAsync(request.Fouders, cancellationToken);
        client.DeleteFounders(founders);
        await _clientRepository.UpdateAsync(client, cancellationToken);
        return client;
    }
}