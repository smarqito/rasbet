namespace DTO.UserDTO;

public class ConfirmationDTO
{
    public string Email { get; set; }
    public string Code { get; set; }

    public ConfirmationDTO(string email, string code)
    {
        Email = email;
        Code = code;
    }
}