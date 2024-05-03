using Application.Commons.Interfaces;
using Domain.Clients;
using Domain.Clients.Exceptions;
using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Clients.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
{
    private readonly IClientRepository _clientRepository;
    private readonly IFounderRepository _founderRepositoy;

    public CreateClientCommandHandler(IClientRepository clientRepository, IFounderRepository founderRepositoy)
    {
        _clientRepository = clientRepository;
        _founderRepositoy = founderRepositoy;
    }

    public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        await CheckInnAsync(request.Inn, cancellationToken);

        var date = new Dates(DateTime.Now);
        var founders = await _founderRepositoy.GetFoundersAsync(request.Founders, cancellationToken);
        if (request.Founders != null && founders.Count() != request.Founders.Count())
        {
            throw new InvalidOperationException("Не все учредители существуют");
        }

        var client = new Client(request.Inn, request.Type, date, request.Name, founders: founders);

        await _clientRepository.CreateClientAsync(client, cancellationToken);

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