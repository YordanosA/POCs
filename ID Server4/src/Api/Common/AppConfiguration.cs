using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Common
{
    public static class AppConfiguration
    {
        public const string BandlayAdmin = "Bandlay.API.full_access";
        public const string BandlayUsers = "Bandlay.API.read_only";
    }


    public static class Initializer
    {
        public static async Task Init(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                var users = new IdentityRole("Admin");
                await roleManager.CreateAsync(users);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var users = new IdentityRole("User");
                await roleManager.CreateAsync(users);
            }

            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                var users = new IdentityRole("Manager");
                await roleManager.CreateAsync(users);
            }
        }
    }
}
