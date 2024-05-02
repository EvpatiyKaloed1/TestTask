using Application.Commons.Interfaces;
using MediatR;

namespace Application.Commands.Clients.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public DeleteClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await _clientRepository.DeleteClientAsync(request.Id, cancellationToken);
    }
}