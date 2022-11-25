using System.Globalization;

namespace DTO.UserDTO;

public class UserSimpleDTO
{
    public string Email { get; set; }
    public string Language { get; set; }
    public string Coin { get; set; }


    public UserSimpleDTO(string email, string language, string coin)
    {
        Email = email;
        Language = language;
        Coin = coin;
    }

}