using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
using Warbud.Revit.Statistics.Factories;
using Warbud.Revit.Statistics.Infrastructure.Context;
using Warbud.Revit.Statistics.Infrastructure.Options;
using Warbud.Revit.Statistics.Infrastructure.Repositories;
using Warbud.Revit.Statistics.Interfaces;
using Warbud.Shared;
using Warbud.Shared.Abstraction.Commands;
using Warbud.Shared.Abstraction.Queries;
using Warbud.Shared.Commands;
using Warbud.Shared.Options;
using Warbud.Shared.Queries;

namespace Warbud.Revit.Statistics
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
            var options = Configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<StatisticContext>(ctx => 
                ctx.UseNpgsql(options.ConnectionString));
            
            services.AddScoped<IStatisticRepository, StatisticRepository>();
            services.AddSingleton<IStatisticFactory, StatisticFactory>();

            services.AddShared();
            services.AddCommands();
            services.AddQueries();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Warbud.Revit.Statistics", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warbud.Revit.Statistics v1"));
            }
            app.UseShared();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}