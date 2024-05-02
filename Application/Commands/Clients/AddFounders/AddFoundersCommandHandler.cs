using Application.Commons.Interfaces;
using Domain.Clients;
using MediatR;

namespace Application.Commands.Clients.AddFounders;

public class AddFoundersCommandHandler : IRequestHandler<AddFoundersCommand, Client>
{
    private readonly IClientRepository _clientRepository;
    private readonly IFounderRepository _founderRepository;

    public AddFoundersCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepository)
    {
        _clientRepository = clientRepository;
        _founderRepository = founderRepository;
    }

    public async Task<Client> Handle(AddFoundersCommand request, CancellationToken cancellationToken)
    {
        var founders = await _founderRepository.GetFoundersAsync(request.Founders, cancellationToken);
        if (request.Founders != null && founders.Count() != request.Founders.Count())
        {
            throw new InvalidOperationException("Не все учредители существуют");
        }
        var client = await _clientRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
        client.AddFounders(founders);
        await _clientRepository.UpdateAsync(client, cancellationToken);
        return client;
    }
}