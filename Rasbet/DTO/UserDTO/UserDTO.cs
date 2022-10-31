namespace DTO.UserDTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Language { get; set; }


    public UserDTO(string name, string email, string language)
    {
        Name = name;
        Email = email;
        Language = language;
    }
}