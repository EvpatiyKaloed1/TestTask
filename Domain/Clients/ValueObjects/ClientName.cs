namespace Domain.Clients.ValueObjects;

public readonly record struct ClientName
{
    public ClientName(string name)
    {
        Validate(name);
        Name = name;
    }
    public string Name { get; }

    private void Validate(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), "Укажите имя");
        }
    }
}