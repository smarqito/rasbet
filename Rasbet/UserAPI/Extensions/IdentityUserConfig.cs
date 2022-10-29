using Domain;
using Microsoft.AspNetCore.Identity;
using UserPersistence;

namespace UserAPI.Extensions;

public static class IdentityUserConfig
{
    public static void AddIdentityUserConfig(this IServiceCollection services)
    {
        var builder = services.AddIdentity<Domain.User, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        });
        var identityBuilder = new IdentityBuilder(builder.UserType, builder.RoleType, builder.Services);
        identityBuilder.AddEntityFrameworkStores<UserContext>();
        identityBuilder.AddSignInManager<SignInManager<Domain.User>>();
        //identityBuilder.AddUserManager(UserManager<Domain.User>);
        identityBuilder.AddDefaultTokenProviders();

    }
}
