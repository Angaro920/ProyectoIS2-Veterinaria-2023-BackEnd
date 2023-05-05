using API_Log.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using System.Reflection;
using System.IO;
using API_Log.Utilities;
using IAuthenticationService = API_Log.Utilities.IAuthenticationService;

namespace API_Log
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
            
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));
            services.Configure<TokenManagement>(Configuration.GetSection("JsonWebTokenKeys"));
            var tokenConfigurations = Configuration.GetSection("JsonWebTokenKeys").Get<TokenManagement>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = tokenConfigurations.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenConfigurations.IssuerSigningKey)),
                    ValidateIssuer = tokenConfigurations.ValidateIssuer,
                    ValidIssuer = tokenConfigurations.ValidIssuer,
                    ValidateAudience = tokenConfigurations.ValidateAudience,
                    ValidAudience = tokenConfigurations.ValidAudience,
                    RequireExpirationTime = tokenConfigurations.RequireExpirationTime,
                    ValidateLifetime = tokenConfigurations.RequireExpirationTime,
                    ClockSkew = TimeSpan.Zero,
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired-Time", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Veterinaria {groupName}",
                    Version = groupName,
                    Description = "APIS de gestion",
                    Contact = new OpenApiContact
                    {
                        Name = "Veterinaria",
                        Email = string.Empty,
                        Url = new Uri("https://Veterinaria.com/"),
                    }
                });
            });
            services.AddScoped<IAuthenticationService, TokenAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Veterinaria_API"); });
            app.UseCors(x => x.WithOrigins("www.google.com", "https://localhost:8085").AllowAnyMethod().AllowAnyHeader());// global cors policy - .WithOrigins("")
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
