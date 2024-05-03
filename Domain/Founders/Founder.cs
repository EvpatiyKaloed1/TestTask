using Domain.Clients.Dto;
using Domain.Common.ValueObjects;
using Domain.Founders.ValueObjects;

namespace Domain.Founders;

public sealed class Founder
{
    public Founder(Inn inn, FounderFullName name, Dates date, Guid? id = null)
    {
        Inn = inn;
        Name = name;
        Date = date;
        Id = id ?? Guid.NewGuid();
    }
    public Inn Inn { get; private set; }
    public FounderFullName Name { get; private set; }
    public Dates Date { get; private set; }
    public Guid Id { get; private set; }

    public void Update(UpdateFounderDto request)
    {
        Inn = request.Inn ?? Inn;
        Date = new Dates(Date.Created, DateTime.Now);
        if (!string.IsNullOrEmpty(request.FirstName))
        {
            Name = new FounderFullName(request.FirstName, Name.LastName, Name.SurName);
        }
        if (!string.IsNullOrEmpty(request.LastName))
        {
            Name = new FounderFullName(Name.FirstName, request.LastName, Name.SurName);
        }
        if (!string.IsNullOrEmpty(request.SurName))
        {
            Name = new FounderFullName(Name.FirstName, Name.LastName, request.SurName);
        }
    }
}