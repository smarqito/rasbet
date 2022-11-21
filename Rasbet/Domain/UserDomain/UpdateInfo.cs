using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class UpdateInfo 
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string IBAN { get; set;}
    public string Password { get; set;}
    public string ConfirmationCode { get; set;}
    public bool Accepted { get; set; } = false;

    protected UpdateInfo (){

    }

    public UpdateInfo(string email, string password, string iban, string phoneno, string code) 
    {
        Email = email;
        Password = password;
        IBAN = iban;
        PhoneNumber = phoneno;
        ConfirmationCode = code;
    }

    public UpdateInfo(string email, string password, string code){
      Email = email;
      Password = password;
      ConfirmationCode = code;
    }
  
}