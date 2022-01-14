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

            //���U�갵���O
            //Singleton
            //�Q��Ҥƫ�N���|�����A�{���B������u�|���@�ӹ�ҡC
            //Scoped
            //�C�� Request �����s new �@�ӷs����ҡA�P�@�� Request ���޸g�L�h�֭� Pipeline ���O�ΦP�@�ӹ�ҡC
            //Transient
            //�C���`�J�ɡA�����s new �@�ӷs����ҡC
            services.AddSingleton<ISingle, Sample>();
            services.AddScoped<IScoped, Sample>();
            services.AddTransient<ITransient, Sample>();

            //�t�Xclass DependencyInjection
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
