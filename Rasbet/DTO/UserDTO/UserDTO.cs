namespace DTO.UserDTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Language { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }


    public UserDTO(string name, string email, string language)
    {
        Name = name;
        Email = email;
        Language = language;
    }

    public UserDTO(string name, string email, string language, string token, string role) : this(name, email, language)
    {
        Token = token;
        Role = role;
    }
}