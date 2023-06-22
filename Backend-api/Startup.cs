using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Hosting;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Hosting;  
  
namespace TuNombreDeProyecto  
{  
    public class Startup  
    {  
        public IConfiguration Configuration { get; }  
  
        public Startup(IConfiguration configuration)  
        {  
            Configuration = configuration;  
        }  
  
        public void ConfigureServices(IServiceCollection services)  
        {  
            services.AddControllers();  
  
            services.AddCors(options =>  
            {  
                options.AddPolicy("AllowSpecificOrigin",  
                    builder =>  
                    {  
                        builder.WithOrigins("https://login-proyecto-angular-master-2.vercel.app")  
                            .AllowAnyHeader()  
                            .AllowAnyMethod();  
                    });  
            });  
        }  
  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
        {  
            if (env.IsDevelopment())  
            {  
                app.UseDeveloperExceptionPage();  
            }  
            else  
            {  
                app.UseExceptionHandler("/Error");  
            }  
  
            app.UseHttpsRedirection();  
  
            app.UseRouting();  
  
            app.UseCors("AllowSpecificOrigin");  
  
            app.UseAuthorization();  
  
            app.UseEndpoints(endpoints =>  
            {  
                endpoints.MapControllers();  
            });  
        }  
    }  
}  
