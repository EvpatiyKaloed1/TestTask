namespace Presentation.Controllers.Client.Dto;

public sealed record AddFounderDto(Guid ClientId, IEnumerable<Guid> Founders);