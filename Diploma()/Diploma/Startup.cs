using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Diploma.Repository;
using Diploma.Repository.BoxingClubsRepository;
using Diploma.Repository.CoachesRepository;
using Diploma.Services;
using Diploma.Services.BoxersComparisonServices;
using Diploma.Services.BoxingClubsServices;
using Diploma.Services.CoachesServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Diploma
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")

                .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                           // укзывает, будет ли валидироваться издатель при валидации токена
                           ValidateIssuer = true,
                           // строка, представляющая издателя
                           ValidIssuer = AuthOptions.ISSUER,

                           // будет ли валидироваться потребитель токена
                           ValidateAudience = true,
                           // установка потребителя токена
                           ValidAudience = AuthOptions.AUDIENCE,
                           // будет ли валидироваться время существования
                           ValidateLifetime = true,

                           // установка ключа безопасности
                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           // валидация ключа безопасности
                           ValidateIssuerSigningKey = true,
                    };
                });




            services.AddControllersWithViews();

            services.AddDbContext<BoxContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BoxContext")));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IBoxersServices, BoxersServices>();
            services.AddScoped<IBoxersRepository, BoxersRepository>();

            services.AddScoped<IBoxingClubsServices, BoxingClubsServices>();
            services.AddScoped<IBoxingClubsRepository, BoxingClubsRepository>();

            services.AddScoped<ICoachesServices, CoachesServices>();
            services.AddScoped<ICoachesRepository, CoachesRepository>();

            services.AddScoped<IBoxersComparisonServices, BoxersComparisonServices>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


                app.Use(async (context, next) =>
                {
                    await next();

                    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized) // 401
                    {
                        context.Response.ContentType = "application/json";


                        await context.Response.WriteAsync(new
                        {
                            Message = "Вы не авторизированны 401"
                        }.ToString());
                    }

                    if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden) // 403
                    {
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(new
                        {
                            Message = "Нет доступа 403"
                        }.ToString());
                    }
                });

            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



           
           



        IList <CultureInfo> supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            

            /*     app.UseStatusCodePages(async statusCodeContext =>
                 {
                     switch (statusCodeContext.HttpContext.Response.StatusCode)
                     {
                         case 401:
                             statusCodeContext.HttpContext.Response.StatusCode = 400;
                             await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new { httpStatus = 500, Message = "some message" });
                             break;
                         case 403:
                             statusCodeContext.HttpContext.Response.StatusCode = 400;
                             await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new  { httpStatus = 500, Message = "some message" });
                             break;
                     }
                 });*/


        }
    }
}