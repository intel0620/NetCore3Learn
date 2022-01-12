using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore3Learn.Data;
using NetCore3Learn.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3Learn
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
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<RouteOptions>(options =>
            {
                //網址全為小寫
                options.LowercaseUrls = true;
                //Query string 全為小寫
                options.LowercaseQueryStrings = true;
                //Helper 產生的網址最後加上斜線
                options.AppendTrailingSlash = true;
            });

            services.Configure<AppSettingsModel>(Configuration.GetSection("AppSettings"));


            //using System.Data;  using Microsoft.Data.SqlClient;
            services.AddTransient<IDbConnection> (db =>new SqlConnection(Configuration.GetConnectionString(Configuration["DB:RUNDB"])));



            //注入其它JSON  檔案在OthierSetting目錄下
            var config = new ConfigurationBuilder()
                       .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "OthierSetting"))
                       .AddJsonFile("othersettings.json")
                       .Build();

            services.Configure <OthierModel> (config.GetSection("FirstA"));


            //補充說明: 如果想取得FirstA以下的CC_Second 內容
            string GetOtherString = config["FirstA:CC_Second"];


            #region 判斷環境註冊不同實做 launchSettings.json
            if (this.Configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value == Environments.Development)
            {
                //services.AddTransient<IEmailSenderService, LocalEmailSenderService>();
            }
            else
            {
                //services.AddTransient<IEmailSenderService, SendGridEmailSenderService>();
            }

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
