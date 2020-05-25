using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Add swagger: please see the detail from AddSwaggerDocumentation method
            services.AddSwaggerDocumentation();
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "Bandlay.API";
                });

            //claims policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("FullAccessPolicy", policy => policy.RequireClaim("Bandlay.API.full_access"));
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Use swagger: please see the detail from UseSwaggerDocumentation method
            app.UseSwaggerDocumentation();
        }
    }
}
