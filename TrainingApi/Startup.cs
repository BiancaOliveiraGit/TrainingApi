using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainingApi.Data;

namespace TrainingApi
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(cfg =>
            {
                var connection = _config["LocalTrainingApiConnectionString"].ToString();
               // var connection = _config["ProductionTrainingApiConnectionString"].ToString();
                cfg.UseSqlServer(connection);
            });
            services.AddScoped<IRepository, Repository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(swag =>
            {
                swag.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "TrainingApi",
                    Description = "Asp.net Core Api for Exercise Training App",
                    TermsOfService = "Stay Strong",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                    {
                        Name = "Bianca Oliveira",
                        Email = "biancaoliveira.apps@gmail.com",
                        Url = "https://biancaoliveira.apps"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.ConfigureCustomExceptionMiddleware();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(swag =>
            {
                swag.SwaggerEndpoint("/swagger/v1/swagger.json", "TrainingApi V1");
            });
        }
    }
}
