namespace Domain.Clients.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Guid id) : base($"Пользователь с id:{id} не найден")
    {
    }
}