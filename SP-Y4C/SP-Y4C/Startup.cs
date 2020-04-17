using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP_Y4C.Data;
using Microsoft.AspNetCore.Mvc;
using SP_Y4C.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SP_Y4C
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
            services.AddDbContext<Y4CDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultUI();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=AdminConsole}/{action=Index}/{id?}");
            });


            //CreateDefaultRoles(services).Wait();
            //CreateDefaultAdminAccount(services).Wait();
        }

        //private async Task CreateDefaultRoles(IServiceProvider services)
        //{
        //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //    string[] roleNames = { "ADMIN", "VOLUNTEER" };

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            //Create the roles and seed them to the database 
        //            await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }
        //}

        //private async Task CreateDefaultAdminAccount(IServiceProvider services)
        //{
        //    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        //    //TODO: Need to change this to be the admin email account for y4c
        //    ApplicationUser admin = await userManager.FindByEmailAsync("info@y4c.org");
        //    if (admin == null)
        //    {
        //        admin = new ApplicationUser()
        //        {
        //            UserName = "info@y4c.org",
        //            Email = "info@y4c.org",
        //        };
        //        await userManager.CreateAsync(admin, "Test123!@#");
        //    }
        //    await userManager.AddToRoleAsync(admin, "ADMIN");
        //    await userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "ADMIN"));
        //}
    }
}