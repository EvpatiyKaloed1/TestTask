namespace Domain.Clients.Exceptions;

internal class InvalidInnException : Exception
{
    public InvalidInnException() : base("Длина Инн может быть только 10 или 12 символов")
    {
    }
}