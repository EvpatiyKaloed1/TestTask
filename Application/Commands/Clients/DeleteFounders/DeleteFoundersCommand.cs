using Domain.Clients;
using MediatR;

namespace Application.Commands.Clients.DeleteFounders;
public sealed record DeleteFoundersCommand(Guid ClientId, IEnumerable<Guid> Fouders) : IRequest<Client>;