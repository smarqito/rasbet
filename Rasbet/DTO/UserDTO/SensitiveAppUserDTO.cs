namespace DTO.UserDTO;

public class SensitiveAppUserDTO
{
    public string Email { get; set; }
    public string IBAN { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public SensitiveAppUserDTO()
    {

    }
    public SensitiveAppUserDTO(string email, string iBAN, string phoneNo, string password) 
    {
        Email = email;
        IBAN = iBAN;
        PhoneNumber = phoneNo;
        Password = password;
    }
}