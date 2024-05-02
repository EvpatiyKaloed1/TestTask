using Application.Commons.Interfaces;
using Domain.Clients;
using MediatR;

namespace Application.Queries.Clients;

public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, IEnumerable<Client>>
{
    private readonly IClientRepository _clientRepository;

    public GetClientListQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Task<IEnumerable<Client>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
    {
        return _clientRepository.GetAllAsync(cancellationToken);
    }
}