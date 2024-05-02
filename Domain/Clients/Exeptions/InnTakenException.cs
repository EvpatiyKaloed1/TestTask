using Domain.Common.ValueObjects;

namespace Domain.Clients.Exeptions;

public class InnTakenException : Exception
{
    public InnTakenException(Inn inn) : base($"ИНН: {inn.InnValue} уже занят")
    {
    }
}