using Retrotracker.Domain;

namespace Retrotracker.Application;

public class UserDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Role Role { get; set; }

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

}