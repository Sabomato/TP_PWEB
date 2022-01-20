using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Data;
using TP_PWEB.Helpers;
using TP_PWEB.Models;
using TP_PWEB.Services;

namespace TP_PWEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);



            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            
            //services.AddTransient<IEmailSender, EmailSender>();
            //services.Configure<AuthMessageSenderOptions>(Configuration);
            

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        /*
        private async Task CreateTestUsers(IServiceProvider serviceProvider)
        {
            var roleNames = Configuration.GetSection("Roles").Get<string[]>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            IdentityUser createdUser;
            var users = new IdentityUser[]
            {
                  new IdentityUser()
                  {
                      Email = "PropertyManager1@email.com",
                      UserName = "PropertyManager1@email.com"
                  },
                 new IdentityUser()
                  {
                      Email = "Client1@email.com",
                      UserName = "Client1@email.com"
                  },
                 new IdentityUser()
                  {
                      Email = "PropertyEmployee1@email.com",
                      UserName = "PropertyEmployee1@email.com"
                  }
            };

            createdUser = await CreateUserWithRole(users[0], UserManager, RoleNames.PropertyOwner);

          
            //context.


            await CreateUserWithRole(users[1], UserManager, RoleNames.Client);
            await CreateUserWithRole(users[2], UserManager, RoleNames.PropertyEmployee);
           

        }
        private async Task<IdentityUser> CreateUserWithRole(IdentityUser user,UserManager<IdentityUser>userManager,string roleName, ApplicationDbContext context)
        {
           var userResult = await userManager.FindByEmailAsync(user.Email);

            if (userResult == null)
            {
                var createUser = await userManager.CreateAsync(user, user.UserName);
                if (createUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await userManager.AddToRoleAsync(user, roleName);

                    var createdUser  = await userManager.FindByEmailAsync(user.Email);

                    context.PropertyManagers.Add(new PropertyManager
                    {
                        User = createdUser
                    });
                    context.Users.
                }
            }
            return null;
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleNames = Configuration.GetSection("Roles").Get<string[]>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityResult roleResult;


            foreach (var roleName in roleNames)
            {


                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            var admin = Configuration.GetSection("Admin");

            var poweruser = new IdentityUser
            {

                 UserName = admin.GetValue<string>("Username"),
                 Email = admin.GetValue<string>("Email"),


            };
            string password = admin.GetValue<string>("Password");
            string email = admin.GetValue<string>("Email");
            string username = admin.GetValue<string>("Username");


            var _user = await UserManager.FindByEmailAsync(poweruser.Email);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, admin.GetValue<string>("Password"));
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
        */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
 

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            DatabaseSeeding.Seed(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                /*endpoints.MapAreaControllerRoute(
                        name: "default",
                        areaName:"Identity",
                        pattern: "{area}/{Page=Account}/{action=Register}"
                    );
                */
            });
        }
    }
}
