using Domain.Clients;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Clients.CreateClient;
public sealed record CreateClientCommand(ClientName Name, Inn Inn, ClientType Type, IEnumerable<Guid>? Founders = null) : IRequest<Client>;