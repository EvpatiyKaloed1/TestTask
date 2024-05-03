using Domain.Clients.ValueObjects;

namespace Domain.Clients.Exceptions;

public class InvalidClientTypeException : Exception
{
    public InvalidClientTypeException(ClientType type) : base("У Индивидуального предпринимателя не может быть учредителей")
    {
    }
}