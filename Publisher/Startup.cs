using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Publisher.AppDbContext;
using Publisher.DTOs;
using Publisher.Models;
using Publisher.Repositories;
using Publisher.Repositories.Implementations;
using Publisher.Repositories.Interfaces;
using Publisher.Services.Implementations;
using Publisher.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher
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
            services.AddDbContext<PublisherContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLSERVER"));
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddScoped<IUnitOfWork, PublisherContext>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Publisher", Version = "v1" });
            });

            //Masstransit config
            services.AddMassTransit(s =>
            {
                s.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration.GetConnectionString("RabbitMq"));
                });
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var ctx = services.GetRequiredService<PublisherContext>();

                if (ctx.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("> Running pending migrations");
                    ctx.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publisher v1"));
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
