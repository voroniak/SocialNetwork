using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using SocialNetwork.Api.Data.Repository.Settings;
using SocialNetwork.Api.Data.Services.Implementation;

namespace SocialNetwork.Api
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

            var mongoDbIdentityConfiguration = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new AspNetCore.Identity.MongoDbCore.Infrastructure.MongoDbSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "MongoDbTests"
                },
                IdentityOptionsAction = options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // ApplicationUser settings
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";
                }
            };
            services.Configure<Data.Repository.Settings.MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
               (IMongoDbSettings)serviceProvider.GetRequiredService<IOptions<Data.Repository.Settings.MongoDbSettings>>().Value);
            //      services.AddIdentity<ApplicationUser, ApplicationRole>()
            //            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>("mongodb://localhost:27017", "MongoDbTests")
            //          .AddDefaultTokenProviders();
            services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfiguration);
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<UserManagerService>();
            services.AddScoped<JwtService>();
            services.AddScoped<PostService>();
            services.AddScoped<LikeService>();
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Place Info Service API",
                    Version = "v2",
                    Description = "Sample service for Learner",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));
        }
    }
}
