namespace DTO.UserDTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Language { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }


    public UserDTO(string id, string name, string email, string language)
    {
        Id = id;
        Name = name;
        Email = email;
        Language = language;
    }

    public UserDTO(string id, string name, string email, string language, string token, string role) : this(id, name, email, language)
    { 
        Token = token;
        Role = role;
    }
}