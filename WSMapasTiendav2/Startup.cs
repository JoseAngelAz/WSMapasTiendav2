using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMapasTiendav2.Models.commons;
using WSMapasTiendav2.Servicios;

namespace WSMapasTiendav2
{
    public class Startup
    {
        readonly string MiCors="MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configuracion de CORS
            services.AddCors(options => {
                options.AddPolicy(name: MiCors,
                                  builder => {
                                      //le decimos que nos acepte todos los headers
                                      builder.WithHeaders("*");
                                      //que acepte todos los origenes
                                      builder.WithOrigins("*");
                                      //para que acepte todos los metodos
                                      builder.WithMethods("*");
                                  });
            });
            services.AddControllers();
            //inyectando Dependencias de IUserServicio y UserServicio por scopped a cada request que hacemos al servicio.
            services.AddScoped<IUserServicio, UserServicio>();
            //Inyectar el secreto
            var appSettingSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSettings>(appSettingSection);
            //JWT -- mandamos a pedir la clase que tiene el secreto
            var appsettings = appSettingSection.Get<AppSettings>();
            //llave
            var llave = Encoding.ASCII.GetBytes(appsettings.Secreto);
            //dar de alta el TOKEN
            services.AddAuthentication(d =>
            {
                //instalar nugget lib de Jwtbearer
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             //parte 2 de JWT, configurar
             .AddJwtBearer(d=> {
                 d.RequireHttpsMetadata = false;
                 d.SaveToken = true;
                 d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(llave),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WSMapasTiendav2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMapasTiendav2 v1"));
            }
            //AGREGAMOS EL USO DE CORS
            app.UseCors(MiCors);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //agregamos autenticacion
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
