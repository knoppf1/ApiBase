using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using apiBase.Core.Interfaces;
using apiBase.Views;
using apiBase.Data;
using apiBase.DataDapper;
using apiBase.Services.Auth;
using apiBase.Services.Specific;
using apiBase.Services.Commons;
using apiBase.Views.BusinessList;
//using apiBase.Views.Enumns;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;

namespace apiBase
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor do controle
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Propriedade de configura��o 
        /// </summary>
        public IConfiguration Configuration { get; }
        private const string Secret = "db3OIsj+BX567FGy0t8W3TcNekrF+2d/1sFnWG4la4okZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        /// <summary>
        /// Inje��o de Depend�ncias - Amarra a INTERFACE ao SERVI�O
        /// </summary>
        /// <param name="services">Cole��o de servi�os</param>
        public void RegistreServices(IServiceCollection services)
        {
            string ipServidor;
           
            //=============== PRODUCAO =======================
            ipServidor = "Server=127.0.0.1;Database=GSInstrumentos;Uid=Root;Pwd=kerstner@123";

            

            services.AddScoped<DataContext>(_ => new DataContext(ipServidor));
            services.AddScoped<DBQuery>(_ => new DBQuery(ipServidor));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IServiceAuthentication, ServiceAuthentication>();
           
            services.AddScoped<IServiceCategoria, ServiceCategoria>();
            services.AddScoped<IServiceCadastro, ServiceCadastro>();
            services.AddScoped<IServiceUsuario, ServiceUsuario>();
            //services.AddScoped<IServiceAuthentication, ServiceAuthentication>();


        }


        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configurador de servicos
        /// </summary>
        /// <param name="services">Cole��o de servi�os</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });

            });

            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
            
            

            //services.AddMvc();
            services.AddRazorPages();

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddSignalR();

            // Configurando o servi�o de documenta��o do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "GSInstrumentos",
                        Version = "v1",
                        Description = "Sistema Gest�o Equipamentos"
                    });
                string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                c.IncludeXmlComments(caminhoXmlDoc);
            });


            RegistreServices(services);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configura��o de aplica��o
        /// </summary>
        /// <param name="app">Aplica��o</param>
        /// <param name="env">Ambiente de hospedagem</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                // Which is the same as the template
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "help";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "ApiBase - Sistema Gestão Equipamentos");
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "uploads")
                ),
                RequestPath = new PathString("/uploads")
            });

        }

        
    }

}
