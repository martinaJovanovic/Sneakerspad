using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Data;

[assembly: HostingStartup(typeof(OnlineShopping.Areas.Identity.IdentityHostingStartup))]
namespace OnlineShopping.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OnlineShoppingContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OnlineShoppingContext")));
            });
        }
    }
}