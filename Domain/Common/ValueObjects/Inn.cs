using Domain.Clients.Exceptions;

namespace Domain.Common.ValueObjects;

public readonly record struct Inn
{
    public string InnValue { get; }

    public Inn(string innValue)
    {
        Validate(innValue);
        InnValue = innValue;
    }
    private void Validate(string inn)
    {
        if (string.IsNullOrEmpty(inn))
        {
            throw new ArgumentNullException(nameof(inn), "ИНН не может равняться null или быть пустым");
        }
        if (inn.Length != 10 && inn.Length != 12)
        {
            throw new InvalidInnException();
        }
    }
}