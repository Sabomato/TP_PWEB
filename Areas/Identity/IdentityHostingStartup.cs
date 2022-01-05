using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TP_PWEB.Data;

[assembly: HostingStartup(typeof(TP_PWEB.Areas.Identity.IdentityHostingStartup))]
namespace TP_PWEB.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<TP_PWEBContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("TP_PWEBContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<TP_PWEBContext>();
            });
        }
    }
}