using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
using eShop.DataAccess.Repositories;
using eShop.Model;
using eShop.UI.Models;
using eShop.UI.UIServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UI
{
    public class Startup
    {
        //For take the JWToken from Cookie
        //private readonly HttpContext _httpContextAccessor;
        public Startup(IConfiguration configuration/*, HttpContext httpContextAccessor*/)
        {
            Configuration = configuration;
            //this._httpContextAccessor = httpContextAccessor;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {       
           
            services.AddDbContext<EshopDbContext>();


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddTransient<ICredentialRepository, CredentialRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddUnitOfWorkServices();
            services.AddRepositoriesServices();

            //services.AddTransient<IEmailSender, EmailSender>();

            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            //configure jwt athentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    //Look for Token in a Cookie
                    //x.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        context.Token = context.Request.Cookies["JWTCookie"];
                    //        return Task.CompletedTask;
                    //    }
                    //};

                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });

            services.AddTransient<ILoginService, LoginService>();


            //Lynei to Error: Unable to resolve service for type 'Microsoft.AspNetCore.Session.ISessionStore' 
            //while attempting to activate 'Microsoft.AspNetCore.Session.SessionMiddleware alla meta vgazei error sthn StartUp gia Not Bearer provide
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //    .AddSessionStateTempDataProvider();

            services.AddSession(options=>options.IdleTimeout = TimeSpan.FromMinutes(30));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddControllersWithViews();
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

            app.UseCookiePolicy();
            app.UseSession();

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseRouting();

            
            app.Use(async (context, next) =>
            {
                #region JWToken with Cookie implementation
                var authenticationCookieName = "JWTCookie";
                var cookie = context.Request.Cookies[authenticationCookieName];
                if (cookie != null)
                {
                    context.Request.Headers.Append("Authorization", "Bearer " + cookie);
                }
                #endregion


                #region JWToken with Session
                //var jWToken = context.Session.GetString("JWToken");
                //if (!string.IsNullOrWhiteSpace(jWToken))
                //{
                //    context.Request.Headers.Add("Authorization", "Bearer " + jWToken);
                //    context.Request.Headers.Add("Content-Type", "application/json");

                //}
                #endregion
                await next();
            });


            //Gia xrhsh tou Authorization Attribute panw apo tous Controller
            app.UseAuthorization();

            app.UseAuthentication();

            //UseMvc gia na paizei to middleware gia to JWToken, me to UseEndpoints den epaize
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // After app.UseEndpoints()
            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapControllerRoute(
            //        name: "admin",
            //        pattern: "{controller=DashboardAdmin}/{action=Index}/{id?}"
            //        );
            //    endpoints.MapControllerRoute(
            //        name: "user",
            //        pattern: "{controller=User}/{action=CreateUser}/{id?}"
            //        );
            //});
        }
    }

    internal static class ServicesExtension
    {
        private static List<Assembly> GetAssemblies()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var myTBBSAssemblies = new List<Assembly>();

            var myTBBSAsseblyFileNames = Directory.GetFiles(path, "eShop.*.dll");

            foreach (var myErpAsseblyFileName in myTBBSAsseblyFileNames)
                myTBBSAssemblies.Add(Assembly.LoadFrom(myErpAsseblyFileName));

            return myTBBSAssemblies;
        }

        internal static void AddUnitOfWorkServices(this IServiceCollection services)
        {
            var assemblies = GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.StartsWith("eShop.DataAccess"))
                {
                    foreach (var assemblyDefinedType in assembly.DefinedTypes)
                    {

                        if (assemblyDefinedType.FullName.StartsWith("eShop.DataAccess") && assemblyDefinedType.IsClass &&
                            assemblyDefinedType.FullName.EndsWith("UnitOfWork"))
                        {
                            var implementType = assemblyDefinedType.AsType();

                            //I default "AService" always implement "IAService", You can custom it



                            Type interfaceType = null;
                            if (assemblyDefinedType.FullName.EndsWith("UnitOfWork") && assemblyDefinedType.Name != "UnitOfWork")
                            {
                                interfaceType = implementType.GetInterface("I" + implementType.Name);
                            }

                            if ((interfaceType != null))
                            {
                                services.AddScoped(interfaceType, implementType);
                            }

                        }
                    }
                }
            }
        }

        internal static void AddRepositoriesServices(this IServiceCollection services)
        {
            var assemblies = GetAssemblies();

            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.StartsWith("eShop.DataAccess"))
                {
                    foreach (var assemblyDefinedType in assembly.DefinedTypes)
                    {

                        if (assemblyDefinedType.FullName.StartsWith("eShop.DataAccess") && assemblyDefinedType.IsClass &&
                            (assemblyDefinedType.FullName.EndsWith("Repository")))
                        {
                            var implementType = assemblyDefinedType.AsType();

                            //I default "AService" always implement "IAService", You can custom it



                            Type interfaceType = null;
                            if (assemblyDefinedType.FullName.EndsWith("Repository"))
                            {
                                interfaceType = implementType.GetInterface("I" + implementType.Name.Replace("Repository", "DbRepository"));
                            }


                            if ((interfaceType != null))
                            {
                                services.AddScoped(interfaceType, implementType);
                            }

                        }
                    }
                }
            }
        }
    }
}
