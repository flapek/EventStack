using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using EventStack_API.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace EventStack_API
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
            services.AddControllers();

            services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));

            services.AddSingleton<IDbSettings>(s => s.GetRequiredService<IOptions<DbSettings>>().Value);
            services.AddScoped<IDbContext, MongoDbContext>();
            services.AddScoped<IRepositoryFactory<Organization>, MongoRepository<Organization>>();
            services.AddScoped<IRepositoryFactory<Category>, Category_MongoRepository>();
            services.AddScoped<IRepositoryFactory<Event>, Event_MongoRepository>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("EventStack", new OpenApiInfo { Title = "DbApi", Version = "v1" });
                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/EventStack/swagger.json", "EventStack v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
