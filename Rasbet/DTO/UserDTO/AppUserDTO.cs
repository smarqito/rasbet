namespace DTO.UserDTO;

public class AppUserDTO : UserDTO
{
    public string IBAN { get; set; }
    public string NIF { get; set; }
    public DateTime DOB { get; set; }
    public string PhoneNumber { get; set; }
    public string Coin { get; set; }
    public bool Notifications { get; set; }

    public AppUserDTO(string name, string language, string email, string iBAN, string nIF, DateTime birthDate, string phoneNumber, bool notifications, string coin) : base(name, email, language)
    {
        IBAN = iBAN;
        NIF = nIF;
        DOB = birthDate;
        PhoneNumber = phoneNumber;
        Notifications = notifications;
        Coin = coin;
    }
}