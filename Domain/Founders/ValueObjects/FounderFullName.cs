namespace Domain.Founders.ValueObjects;

public readonly record struct FounderFullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string SurName { get; }
    public string FullName => $"{FirstName} {LastName} {SurName}";
    public FounderFullName(string firstName, string lastName, string surName)
    {
        Validate(firstName, lastName, surName);
        FirstName = firstName;
        LastName = lastName;
        SurName = surName;
    }

    private void Validate(string firstName, string lastName, string surName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            throw new ArgumentNullException(nameof(firstName), "Введите ваше Имя");
        }
        if (string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentNullException(nameof(lastName), "Введите вашу Фамилию");
        }
        if (string.IsNullOrEmpty(surName))
        {
            throw new ArgumentNullException(nameof(surName), "Введите ваше Отчество");
        }
    }
}