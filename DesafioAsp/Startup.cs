using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using DesafioAsp.Data.Configurations;
using DesafioAsp.Data.Repositories;

namespace DesafioAsp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfig>(Configuration.GetSection(nameof(DatabaseConfig))); // Criando Service para a config do DB
            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

            services.AddSingleton<InterfacePokemon, PokemonRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager.API", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}