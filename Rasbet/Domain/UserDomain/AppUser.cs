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
    public DateTime DOB { get; set; }
    public virtual Wallet Wallet { get; set; } = new();
   // public virtual ICollection<Bet> BetHistory { get; set; } = new List<Bet>();
    public string Coin { get; set; } = "EUR";
    public bool Notifications { get; set; }

    protected AppUser() : base()
    {

    }

    public AppUser(string name, string email, string nIF, DateTime dob, string language, bool notifications) : base(name, email, language)
    {
        NIF = nIF;
        DOB = dob;
        Notifications = notifications;
    }

    public AppUser(string name, string email, string nIF, string phoneNumber, DateTime dob, string iBAN, string language, string coin, bool notifications) : base(name, email, language)
    {
        NIF = nIF;
        PhoneNumber = phoneNumber;
        DOB = dob;
        IBAN = iBAN;
        Coin = coin;
        Notifications = notifications;
    }
}