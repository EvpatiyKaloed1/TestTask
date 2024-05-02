namespace Domain.Clients.Exeptions;

internal class InvalidInnExeption : Exception
{
    public InvalidInnExeption() : base("Длина Инн может быть только 10 или 12 символов")
    {
    }
}