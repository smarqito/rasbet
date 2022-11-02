using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{

	public int Id { get; set; }
    public string Name { get; set; }
    public string Language { get; set; } 
    public string Email { get; set; }

    public User(string name, string email, string language)
    {
        Name = name;
        Email = email;
        Language = language;
        UserName = email;
    }
}