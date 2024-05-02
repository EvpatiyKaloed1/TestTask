using Domain.Clients;
using MediatR;

namespace Application.Queries.Clients;

public sealed record GetClientListQuery() : IRequest<IEnumerable<Client>>;