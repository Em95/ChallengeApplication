using System;
using System.IO;
using System.Reflection;
using ChallengeApplication.Clients;
using ChallengeApplication.Middleware;
using ChallengeApplication.Services.Implementation;
using ChallengeApplication.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ChallengeApplication
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
            services.AddHttpContextAccessor();

            //Should be separated to service installer
            services.AddTransient<IDataManipulationService, DataManipulationService>();

            //Should be separated to swagger installer
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "C# Challenge API V1", Version = "v1" });

                var xmlSwaggerFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlSwaggerFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.AddHttpClient<IAdformClient, AdformClient>(options =>
            {
                options.BaseAddress = new Uri(Configuration.GetSection("AdformClient:ApiBaseUrl").Value);
                options.DefaultRequestHeaders.Add("Accept",
                    "application/json");
            });
            services.AddHttpClient<IOAuthClient, OAuthClient>(options =>
            {
                options.BaseAddress = new Uri(Configuration.GetSection("OAuthClient:BaseUrl").Value);
                options.DefaultRequestHeaders.Add("Accept",
                    "application/x-www-form-urlencoded");
            });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "C# Challenge API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseOAuthAccesHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
