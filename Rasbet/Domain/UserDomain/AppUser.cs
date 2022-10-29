using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class AppUser : User
{
    public string IBAN { get; set; }
    public string NIF { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual Wallet Wallet { get; set; } = new();

    public AppUser(string name, string email, string nIF, string phoneNumber) : base(name, email)
    {
        NIF = nIF;
        PhoneNumber = phoneNumber;
    }
}