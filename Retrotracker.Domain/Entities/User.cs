namespace Retrotracker.Domain;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime LastLogin { get; set; }

    public User(string name, string password)
    {
        Id = new Guid();
        Name = name;
        Password = password;
    }

    public override string ToString()
    {
        return $"User Id:{Id}, Name: {Name}, Last logged on: {LastLogin}";
    }

}