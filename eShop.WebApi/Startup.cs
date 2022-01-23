using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
using eShop.DataAccess.Repositories;
using eShop.Model;
using eShop.WebApi.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace eShop.WebApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EshopDbContext>();
            
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICustomManager, JwtTokenManager>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddTransient<ICredentialRepository, CredentialRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            services.AddUnitOfWorkServices();
            services.AddRepositoriesServices();
                        

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Specify what in the JWT needs to be checked 
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("xVigzSeBiGNefeojZro3"))
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddControllers().AddNewtonsoftJson(opt => { opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseCors(policy=>
            //policy.WithOrigins("http://localhost:25770","https://localhost:44337")
            //                   .AllowAnyHeader()
            //                   .AllowAnyMethod());


            app.UseCors("Open");

            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
