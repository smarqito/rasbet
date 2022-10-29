namespace DTO.UserDTO;

public class AppUserDTO : UserDTO
{
    public string IBAN { get; set; }
    public string NIF { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }

    public AppUserDTO(string name, string language, string email, string iBAN, string nIF, DateTime birthDate, string phoneNumber) : base(name, language, email)
    {
        IBAN = iBAN;
        NIF = nIF;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
    }
}