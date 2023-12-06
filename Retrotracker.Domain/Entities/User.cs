namespace SocialX.Domain;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<Publication> Publications { get; set; }
    public List<Guid> Following { get; set; }
    public List<Guid> Followers { get; set; }
    public DateTime LastLogin { get; set; }

    public User(string name, string password)
    {
        Id = new Guid();
        Name = name;
        Password = password;
        Publications = new List<Publication>();
        Followers = new List<Guid>();
        Following = new List<Guid>();
    }

    public override string ToString()
    {
        return $"User Id:{Id}, Name: {Name}, Number of publications: {Publications.Count}";
    }

}