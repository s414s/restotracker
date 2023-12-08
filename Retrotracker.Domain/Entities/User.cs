using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class User : IHasId
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.worker;
    [JsonIgnore]
    public string Username
    {
        get
        {
            return $"{Name.ToLower()}{Surname.ToLower()}";
        }
    }

    public User() { }
    public User(string name, string surname, string password)
    {
        Id = new Guid().ToString();
        Name = name;
        Surname = surname;
        Password = password;
    }
}