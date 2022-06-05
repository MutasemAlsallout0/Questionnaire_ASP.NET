using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Questionnaire.Data;
using Questionnaire.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire
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
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddTransient<IManageRolees, ManageRoles>();

            services.AddScoped<IManageRolees, ManageRoles>();
            services.AddScoped<IManageUsers, ManageUsers>();


            services.AddDbContext<QuestionDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QuesDbContext")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<QuestionDbContext>().
                AddDefaultUI();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Add", policy =>
                {
                    policy.RequireClaim("Add");
                });

                options.AddPolicy("Edit", policy =>
                {
                    policy.RequireClaim("Edit");
                });

                options.AddPolicy("Delete", policy =>
                {
                    policy.RequireClaim("Delete");
                });
                options.AddPolicy("View", policy =>
                {
                    policy.RequireClaim("View");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
