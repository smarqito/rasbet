namespace DTO.UserDTO;

public class UpdatePasswordDTO
{
    public string Email { get; set; }
    public string Password { get; set; }


    public UpdatePasswordDTO(string email, string password){
        Email = email;
        Password = password;
    }
}