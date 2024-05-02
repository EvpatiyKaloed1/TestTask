using Domain.Clients.Dto;
using Domain.Clients.Exeptions;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Founders;

namespace Domain.Clients;

public sealed class Client
{
    private List<Founder>? _founders;

    public Inn Inn { get; private set; }
    public ClientName Name { get; private set; }
    public ClientType Type { get; private set; }
    public Dates Date { get; private set; }
    public IReadOnlyList<Founder>? Founders => _founders?.AsReadOnly();
    public Guid Id { get; private set; }

    public Client(Inn inn, ClientType type, Dates dates, ClientName name, Guid? id = null, IEnumerable<Founder>? founders = null)
    {
        Validate(type, founders);

        Inn = inn;
        Type = type;
        Date = dates;
        Name = name;
        Id = id ?? Guid.NewGuid();
        if (founders != null)
        {
            _founders = founders.ToList();
        }
        else
        {
            _founders = null;
        }
    }

    public void Update(UpdateClientDto request)
    {
        Inn = request.Inn ?? Inn;
        Name = request.Name ?? Name;
        Type = request.Type ?? Type;

        if(Type == ClientType.Individual)
        {
            _founders = null;
        }
        if(Type == ClientType.LegalEntity)
        {
            if(request.Founder == null)
            {
                throw new InvalidOperationException("При переводе в статус ЮЛ необходимо указать учредителя");
            }

            _founders = new List<Founder> { request.Founder };
        }

        Date = new Dates(Date.Created, DateTime.Now);
    }

    public void AddFounders(IEnumerable<Founder> founders)
    {
        if (Type == ClientType.Individual)
        {
            throw new InvalidOperationException("Нельзя добавлять учредителей к ИП");
        }

        _founders.AddRange(founders);
    }

    public void DeleteFounders(IEnumerable<Founder> founders)
    {
        if (Type == ClientType.Individual)
        {
            throw new InvalidOperationException("Нельзя удалять учредителей у ИП");
        }

        _founders.RemoveAll(x => founders.Contains(x));
    }

    private void Validate(ClientType type, IEnumerable<Founder>? founders)
    {
        if (type == ClientType.Individual && (founders?.Any() != false && founders != null))
        {
            throw new InvalidClientTypeException(type);
        }
        if (founders == null && type == ClientType.LegalEntity)
        {
            throw new ArgumentNullException(nameof(type), "У ЮЛ должны быть учредители");
        }
    }
}