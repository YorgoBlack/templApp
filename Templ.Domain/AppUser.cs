namespace Templ.Domain;

public interface HasUserName
{
    string UserName { get; }
}

public class AppUser : HasUserName
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    
    public string? LastName { get; set; }

    public string SurName => $"{FirstName} {LastName}";
}
