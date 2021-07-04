using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BapApi.Models;

namespace BapApi
{
    public class Startup
        {
        /// <summary>
        /// [1] IConfiguration
        /// Extension Methods Bind IConfiguration Object Attempts to bind the given object instance to configuration values,
        /// by matching property names against configuration keys recursively.
        /// 
        /// Extension methods enable you to add methods to existing types without creating a new derived type, recompiling,
        /// or otherwise modifying the original type. Extension methods are static methods, but they're called as if they were
        /// instance methods on the extended type. For client code written in C#, F# and Visual Basic, there's no apparent difference
        /// between calling an extension method and the methods defined in a type.
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-5.0
        /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        /// 
        /// [2] IServiceCollection
        /// Registers an action used to configure all instances of a particular type of options. ConfigureOptions(IServiceCollection, Object)
        /// Registers an object that will have all of its I[Post]ConfigureOptions registered. ConfigureOptions(IServiceCollection, Type)
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-5.0
        /// 
        /// 
        /// [3] ConfigureServices
        /// The Dependency Injection pattern is used heavely in ASP.NET Core architecture.It includes built-in IoC container to provide
        /// dependent objects using constructors. the ConfigureServices method is a place where you can register your dependent classes 
        /// with the built-in IoC container. After registering dependent class, it can be used anywhere in the application.
        /// You just need to include it in the parameter of the constructor of a class where you want to use it.The IoC container will
        /// inject it automatically, ASP.NET Core refers dependent class as a Service.So, whenever you read "Service" then understand 
        /// it as a class which is going to be used in some other class, ConfigureServices method includes IServiceCollection parameter 
        /// to register services to the IoC container.Learn more about it in the next chapter.
        /// </summary>

        /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<StoreAppsContext>(opt => opt.UseInMemoryDatabase("StoreApps"));
            services.AddEntityFrameworkSqlite().AddDbContext<StoreAppsContext>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}