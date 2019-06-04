using Lamb_Ji_UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lamb_Ji_UI.Startup))]
namespace Lamb_Ji_UI
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateUsers();
        }

        public void CreateUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "fallndiack1@gmail.com";
            user.UserName = "Fall";
            var check = userManager.Create(user, "Admin@!123");

            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");               
            }

           
        }

        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Visiteur"))
            {
                role = new IdentityRole();
                role.Name = "Visiteur";
                roleManager.Create(role);
            }
        }
    }
}
