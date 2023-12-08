using Retrotracker.Domain;

namespace Retrotracker.Application;

public class UserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public User MapToDomainEntity()
    {
        return new User
        {
            Name = Name,
            Surname = Surname,
            Role = Role
        };
    }

    public static UserDTO MapFromDomainEntity(User user)
    {
        return new UserDTO
        {
            Name = user.Name,
            Surname = user.Surname,
            Role = user.Role,
        };
    }

    public override string ToString() => $"Name {Name}, Surname: {Surname}, Role: {Role}";
}