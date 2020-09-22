///////////////////////////////////////////////////////////////////////////
///
/// Project:        Sprint 1
/// File Name:      Startup.cs
/// Description:    
///                 Contains startup class used to start API
/// Course:         CSCI 4350 - Software Engineering
/// Authors:        
///                 Darien Roach,   roachda@etsu.edu,   Developer
///                 Grant Watson,   watsongo@etsu.edu,  Developer
///                 Stephen Dalton, daltonsa@etsu.edu,  Developer
///                 Kelly King,     kingkr1@etsu.edu,   Developer
///                 Jackson Brooks, brooksjt@etsu.edu,  Developer
///                 Nick Ehrhart,   ehrhart@etsu.edu,   Product Owner
///                 Anna Cade,      cadea1@etsu.edu,    Scrum Master
///                 
/// Created:        Monday, September 14th, 2020
///
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MetricsAPI.Models;
using MySql.Data.MySqlClient;
using MySql.Data.EntityFrameworkCore.Extensions;


namespace MetricsAPI
{
    /// <summary>
    /// Startup class serves to start the API
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Overloaded constructor with 1 parameter
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Simple get method
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MetricContext>(opt =>
              opt.UseMySQL(Configuration.GetConnectionString("Default")));  //login and server credentials located in appsettings.json
            //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
            services.AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application</param>
        /// <param name="env">Enviroment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
