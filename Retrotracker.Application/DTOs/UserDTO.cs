using Retrotracker.Domain;

namespace Retrotracker.Application;

public class UserDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public User MapToDomainEntity()
    {
        return new User
        {
            Name = Name,
            Surname = Surname,
            Password = Password,
            Role = Role
        };
    }

    public static UserDTO MapFromDomainEntity(User user)
    {
        return new UserDTO
        {
            Name = user.Name,
            Surname = user.Surname,
            Password = user.Password,
            Role = user.Role,
        };
    }

}