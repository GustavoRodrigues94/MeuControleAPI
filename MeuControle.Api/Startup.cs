using System;
using System.Text;
using MeuControle.Dominio.Consultas.IndicadorConsultas;
using MeuControle.Dominio.Manipuladores;
using MeuControle.Dominio.Repositorios;
using MeuControle.Infra.Contextos;
using MeuControle.Infra.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MeuControle.Api
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
            services.AddControllers();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuracoes.Segredo)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MeuControle - Controle suas finanças de forma simples",
                        Version = "v1",
                        Description = "Exemplo de API REST criada com o ASP.NET Core 3.1 para o aplicativo MeuControle",
                        Contact = new OpenApiContact
                        {
                            Name = "Gustavo Rodrigues",
                            Url = new Uri("https://github.com/GustavoRodrigues94")
                        }
                    });
            });

            services.AddDbContext<ContextoDados>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<UsuarioManipulador, UsuarioManipulador>();

            services.AddTransient<IPlanoContaRepositorio, PlanoContaRepositorio>();
            services.AddTransient<PlanoContaManipulador, PlanoContaManipulador>();

            services.AddTransient<ILancamentoContaRepositorio, LancamentoContaRepositorio>();
            services.AddTransient<LancamentoContaManipulador, LancamentoContaManipulador>();

            services.AddTransient<IIndicadorRepositorio, IndicadorRepositorio>();
            services.AddTransient<IIndicadorConsulta, IndicadorConsulta>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeuControle V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
