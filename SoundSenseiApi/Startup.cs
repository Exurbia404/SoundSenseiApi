using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend;
public class Startup
{
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        services.AddSwaggerGen();

        services.AddDbContext<SoundSenseiContext>(options =>
            options.UseMySql(_configuration.GetConnectionString("DefaultConnection"), serverVersion));

        services.AddCors(options =>
        {
            options.AddPolicy(name: "_myAllowSpecificOrigins",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        services.AddControllers();

        // Configure Swagger generator
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Title v1"));
        app.UseDeveloperExceptionPage();
        

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("_myAllowSpecificOrigins");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}