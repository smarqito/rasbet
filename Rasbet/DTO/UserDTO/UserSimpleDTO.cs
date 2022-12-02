using System.Globalization;

namespace DTO.UserDTO;

public class UserSimpleDTO
{
    public string Email { get; set; }
    public string Language { get; set; }
    public string Coin { get; set; }
    public bool Notifications { get; set; }

    public UserSimpleDTO()
    {
    }

    public UserSimpleDTO(string email, string language, string coin, bool notifications)
    {
        Email = email;
        Language = language;
        Coin = coin;
        Notifications = notifications;
    }
}