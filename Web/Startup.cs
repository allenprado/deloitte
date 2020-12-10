using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.InterfaceOpenApp;
using Application.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceRepositories;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Infra.Configuration;
using Infra.Repository.Generics;
using Infra.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
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

            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(2000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //HTTP context for sessions
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Generic D.I.
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));

            //Interface and Repository
            services.AddSingleton<IRepositoryUser, RepositoryUser>();
            services.AddSingleton<IRepositoryToDoList, RepositoryToDoList>();

            //Interface and Application
            services.AddSingleton<IUserApp, UserApp>();
            services.AddSingleton<IToDoListApp, ToDoListApp>();

            //Service Domain
            services.AddSingleton<IServiceUser, ServiceUser>();
            services.AddSingleton<IServiceToDoList, ServiceToDoList>();

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

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}");
            });

        }
    }
}
