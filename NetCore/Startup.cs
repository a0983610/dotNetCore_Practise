using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.Dao;
using NetCore.Models;
using NetCore.Service;

namespace NetCore
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

            //註冊實做類別
            //Singleton
            //被實例化後就不會消失，程式運行期間只會有一個實例。
            //Scoped
            //每個 Request 都重新 new 一個新的實例，同一個 Request 不管經過多少個 Pipeline 都是用同一個實例。
            //Transient
            //每次注入時，都重新 new 一個新的實例。
            services.AddSingleton<ISingle, Sample>();
            services.AddScoped<IScoped, Sample>();
            services.AddTransient<ITransient, Sample>();

            //配合class DependencyInjection
            services.AddMember2Service(Configuration);

            //services.AddScoped<IMember2Service, Member2Service>(x =>
            //{
            //    var ConnectionStrings = Configuration.GetValue<string>("ConnectionStrings:netCore");
            //    return new Member2Service(new Member2Dao(ConnectionStrings));
            //});


            //services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
