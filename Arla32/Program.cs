using Arla32.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Identity.Client;
using System.Security.Policy;
using Arla32.Services;
using System.Globalization;

namespace Arla32
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var googleDriveService = new GoogleDriveService();


            builder.Services.AddDbContext<BancoContext>
              (options => options.UseMySql(
                  "server=novolab.c82dqw5tullb.sa-east-1.rds.amazonaws.com;user id=sistema;password=7847awse;database=labdados",
                  Microsoft.EntityFrameworkCore.ServerVersion.Parse("13.2.0-mysql")));

            builder.Services.AddDbContext<QuimicoContext>(options =>
                options.UseMySql(
          "server=novolab.c82dqw5tullb.sa-east-1.rds.amazonaws.com;user id=sistema;password=7847awse;database=quimico",
          Microsoft.EntityFrameworkCore.ServerVersion.Parse("13.2.0-mysql")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
                {
                    option.LoginPath = "/Acess/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
            builder.Services.AddScoped<GoogleDriveService>();

            //passando de ponto para virgula no sistema, forma padrao.
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;

            });
            //termina aqui.


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Acess}/{action=Login}/{id?}");

            app.Run();
        }
    }
}