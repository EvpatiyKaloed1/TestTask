using Domain.Clients;
using MediatR;

namespace Application.Queries;

public sealed record GetClientListQuery() : IRequest<IEnumerable<Client>>;