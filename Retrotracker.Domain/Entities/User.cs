namespace Retrotracker.Domain;
public class User : IHasId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public DateTime LastLogin { get; set; }

    public User() { }

    public User(string name, string password)
    {
        Id = new Guid().ToString();
        Name = name;
        Password = password;
    }

    public override string ToString()
    {
        return $"User Id:{Id}, Name: {Name}, Last logged on: {LastLogin}";
    }

}