using Domain.Founders;
using MediatR;

namespace Application.Queries.Founders;

public sealed class GetFounderListQuery : IRequest<IEnumerable<Founder>>;