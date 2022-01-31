using DesafioSemanal.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesafioSemanal.Repositories;
using DesafioSemanal.Interfaces;
using Newtonsoft.Json;
using DesafioSemanal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DesafioSemanal
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
            //agregamos este servicio para ignorar las relaciones circulares
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioSemanal", Version = "v1" });

                
                //agregado para limitar acceso a swagger
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Ingrese Bearer [Token] para poder autenticarse en la aplicación"
                    });

                c.AddSecurityRequirement( new OpenApiSecurityRequirement 
                {
                    { 
                        new OpenApiSecurityScheme
                        { 
                            Reference= new OpenApiReference
                            { 
                                Type= ReferenceType.SecurityScheme,
                                Id="Bearer"

                            }
                        },
                        new List<string>()
                    }
                });
                
                //--------------------------
            });

            //agregado para las dependencias de entity framework con sql
            services.AddEntityFrameworkSqlServer();
            //agregar nuestro dbContext
            services.AddDbContext<BlogContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                //agregamos el connection string en appsettings y lo llamamos en la siguiente linea
                options.UseSqlServer(Configuration.GetConnectionString("BlogConnection"));

            });

            //agregamos las dependencias o servicios
            //inyectamos la interfaz con su implementacion
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            
            //agregamos contexto del usuario para identity
            services.AddDbContext<UserContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                //agregamos el connection string en appsettings y lo llamamos en la siguiente linea
                options.UseSqlServer(Configuration.GetConnectionString("UserConnection"));

            });

            //agregado para usar identity en nuestro sistema de login
            services.AddIdentity<UserIdentity, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            
            //agregamos autenticacion y su configuracion
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = "https://localhost:5001",
                            ValidIssuer = "https://localhost:5001",
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeAutorizacion"))
                    };
                 });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioSemanal v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //agregado
            app.UseAuthentication();
            //-------------

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
