using System.Text.Json.Serialization;

namespace Retrotracker.Domain;
public class User : IHasId
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("surname")]
    public string Surname { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("role")]
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