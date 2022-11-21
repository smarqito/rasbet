using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Language { get; set; } 

    protected User ()
    {

    }
    public User(string name, string email, string language)
    {
        Name = name;
        Email = email;
        Language = language;
        UserName = email;
    }
}