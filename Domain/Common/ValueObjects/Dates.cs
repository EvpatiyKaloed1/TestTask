using Domain.Clients.Exceptions;

namespace Domain.Common.ValueObjects;

public readonly record struct Dates
{
    public DateTime Created { get; }
    public DateTime? Updated { get; }
    private void Validate(DateTime created, DateTime? updated = null)
    {
        if (created >= updated)
        {
            throw new InvalidDateException(created, updated);
        }
    }
    public Dates(DateTime created, DateTime? updated = null)
    {
        Validate(created, updated);
        Created = created;
        Updated = updated;
    }
}