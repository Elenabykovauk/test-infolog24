using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using UsersApi.Domain;

namespace UsersApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPositionDataService, PositionDataService>();
            services.AddTransient<IUserDataService, UserDataService>();
            
            services.AddControllers();
            services.AddCors(o => o.AddPolicy("AllowOrigins", builder =>
            {
                builder.WithOrigins("http://localhost:3004")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            BsonClassMap.RegisterClassMap<User>(m =>
            {
                m.AutoMap();
            });
            BsonClassMap.RegisterClassMap<Position>(m =>
            {
                m.AutoMap();
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}