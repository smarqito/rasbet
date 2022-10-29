namespace DTO.UserDTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Language { get; set; }
    public string Email { get; set; }

    public UserDTO(string name, string language, string email)
    {
        Name = name;
        Language = language;
        Email = email;
    }
}