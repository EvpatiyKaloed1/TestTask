namespace Domain.Clients.Exceptions;

public class InvalidDateException : Exception
{
    public InvalidDateException(DateTime created, DateTime? updated) : base($"Дата создания:{created} не может быть больше даты обновления:{updated}")
    {
    }
}