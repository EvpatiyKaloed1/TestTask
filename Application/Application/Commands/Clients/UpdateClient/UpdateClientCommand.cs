using Domain.Clients;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Clients.UpdateClient;
public sealed record UpdateClientCommand(Guid Id,
                                         Inn? Inn,
                                         ClientType? Type,
                                         ClientName? Name) : IRequest<Client>;