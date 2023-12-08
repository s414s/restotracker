using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class User : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public string Username
    {
        get
        {
            return $"{Name.ToLower()}{Surname.ToLower()}";
        }
    }
    public Role Role { get; set; }
    public DateTime LastLogin { get; set; }

    public User() { }

    public User(string name, string surname, string password)
    {
        Id = new Guid().ToString();
        Name = name;
        Surname = surname;
        Password = password;
    }

    public override string ToString() => $"User Id:{Id}, Name: {Name}, Last logged on: {LastLogin}";
}