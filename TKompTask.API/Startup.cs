using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TKompTask.API.Services;
using TKompTask.API.Settings;
using TKompTask.DataAccess;

namespace TKompTask.API
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
            // Konfiguracja DataAccess
            services.AddTransient<IDbConnection, SqlConnection>((sp) =>
            {
                var config = sp.GetService<IConfiguration>();
                var conStr = config["ConnectionStrings:Default"]; // Pobieramy template - póxniej zostanie on przetworzony przez DataAccessService
                return new SqlConnection(conStr);
            });
            var authSettings = new AuthSettings();
            Configuration.GetSection("AuthSettings").Bind(authSettings);
            services.AddSingleton<AuthSettings>(authSettings);
            services.AddTransient<IDataAccessService, DataAccessService>(); // <- Tu główna implementacja DAL. Znacznie uproszczona na potrzeby zadania, ale wiadomo, o co chodzi
            services.AddMediatR(typeof(IDataAccessService).Assembly); // <- Wzorzec Mediator

            services.AddControllers(c =>
            {
                c.Filters.Add<ApiAuthFilter>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TKompTask.API", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TKompTask.API v1"));
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
