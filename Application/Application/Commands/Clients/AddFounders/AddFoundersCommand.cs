using Domain.Clients;
using MediatR;

namespace Application.Commands.Clients.AddFounders;
public sealed record AddFoundersCommand(Guid ClientId, IEnumerable<Guid> Founders) : IRequest<Client>;