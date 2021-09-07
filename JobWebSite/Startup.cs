using JobWebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobWebSite.Startup))]
namespace JobWebSite
{
    public partial class Startup
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            IdentityRole role;

            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }


            var user = new ApplicationUser();

            user.Email = "abanobyoussef1996@gmail.com";

            user.UserName = "Abanob Youssef";

            var check = userManager.Create(user, "Hana@1996"); // THE STRING IS THE PASSSWORD  user i added to the table

            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }

        }
    }
}
