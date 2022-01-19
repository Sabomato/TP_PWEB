using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Data;
using TP_PWEB.Models;
using TP_PWEB.Models.Users;

namespace TP_PWEB.Helpers
{
    public static class DatabaseSeeding
    {

        public static async Task Seed( IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetService<ApplicationDbContext>();

            await CreateRoles( serviceProvider);
            await CreateTestUsers(serviceProvider);
            await CreateCategories(context);
            await context.SaveChangesAsync();

        }

        private static async Task CreateTestUsers(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            IdentityUser createdUser;
            
            var users = new IdentityUser[]
            {
                 new IdentityUser()
                  {
                      Email = "Admin1@email.com",
                      UserName = "Admin1@email.com"
                  },
                  new IdentityUser()
                  {
                      Email = "PropertyManager1@email.com",
                      UserName = "PropertyManager1@email.com"
                  },
                 new IdentityUser()
                  {
                      Email = "PropertyEmployee1@email.com",
                      UserName = "PropertyEmployee1@email.com"
                  },
                 new IdentityUser()
                  {
                      Email = "Client1@email.com",
                      UserName = "Client1@email.com"
                  }
            };

            for(int i = 0; i < users.Length; ++i)
            {
                createdUser = await CreateUserWithRole(users[i], UserManager, RoleNames.Roles[i]);
                
                if(createdUser != null)
                {
                    var role = await UserManager.GetRolesAsync(createdUser);
                    ;
                    switch (role.FirstOrDefault())
                    {
                        case RoleNames.Admin:
                            context.Admins.Add(new Admin()
                            {
                                AdminId = createdUser.Id,
                                User = createdUser
                            });
                            break;

                        case RoleNames.Client:
                            context.Clients.Add(new Client()
                            {
                                ClientId = createdUser.Id,
                                User = createdUser
                            });
                            break;

                        case RoleNames.PropertyOwner:
                            context.PropertyManagers.Add(new PropertyManager()
                            {
                                PropertyManagerId = createdUser.Id,
                                User = createdUser
                            });
                            break;

                        case RoleNames.PropertyEmployee:

                            string managerEmail =  String.Format("PropertyManager{0}@email.com",(int)(i/4 + 1));
                            var manager = await UserManager.FindByEmailAsync(managerEmail);

                            context.PropertyEmployees.Add(new PropertyEmployee()
                            {
                                PropertyEmployeeId = createdUser.Id,
                                User = createdUser,
                                PropertyManagerId = manager.Id
                            });
                            break;

                    }
                }

            }

            


            //context.


            await CreateUserWithRole(users[1], UserManager, RoleNames.Client);
            await CreateUserWithRole(users[2], UserManager, RoleNames.PropertyEmployee);


        }
        private static async Task<IdentityUser> CreateUserWithRole(IdentityUser user, UserManager<IdentityUser> userManager, string roleName)
        {
            var userResult = await userManager.FindByEmailAsync(user.Email);

            if (userResult == null)
            {
                var createUser = await userManager.CreateAsync(user, user.UserName);
                if (createUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await userManager.AddToRoleAsync(user, roleName);

                    return await userManager.FindByEmailAsync(user.Email);

                }
            }
            return null;
        }

        private static async Task CreateRoles( IServiceProvider serviceProvider)
        {
            var roleNames = RoleNames.Roles;
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

        }

        private static async Task CreateCategories(ApplicationDbContext context)
        {
            Category[] categories =
            {
                new Category{
                    
                    Name = "Hotel",
                },
                new Category{
                    
                    Name = "Hostel",
                },
                new Category{
                    
                    Name = "Apartment",
                }
            };
            foreach (var category in categories)
            {
                if(context.Categories.FirstOrDefault(c => c.Name == category.Name) == null)
                    await context.Categories.AddAsync(category);
            }


        }
    }
    }
