using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using CloudAuditionApi.DatabaseService;
using CloudAuditionApi.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CloudAuditionApi
{
    public class Startup
    {
        protected readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();

            string databaseName = Configuration["POSTGRES_DB"];
            string userName = Configuration["POSTGRES_USER"];
            string password = Configuration["POSTGRES_PASSWORD"];

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<CloudAuditionApiContext>(
                   options => options.UseNpgsql(
                       $"host=db;database={databaseName};user id={userName};password={password};"
                   )
               )
               .BuildServiceProvider();

            services.AddMvc().AddFluentValidation().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IMessageDbService, MessageDbService>();
            services.AddTransient<IValidator<Message>, MessageValidator>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CloudAuditionApiContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
