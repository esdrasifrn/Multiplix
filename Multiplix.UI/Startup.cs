using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Services;
using Multiplix.Infrastructure.Data;
using Multiplix.Infrastructure.RepositoryEF;
using Multiplix.UI.Utils;
using QuizCorp.Domain.Services;

namespace Multiplix.UI
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
            #region Conection
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<MultiplixContext>(
                       options => options.UseSqlServer(
                           Configuration.GetConnectionString("MultiplixConnectionString")));
            #endregion

            #region Injeções de dependência
            services.AddScoped<GlobalFilter>();
            services.AddScoped<IServiceUsuario, ServiceUsuario>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IServiceGrupo, ServiceGrupo>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();

            services.AddScoped<IServicePermissao, ServicePermissao>();
            services.AddScoped<IPermissaoRepository, PermissaoRepository>();

            services.AddScoped<IServicePatrocinador, ServicePatrocinador>();
            services.AddScoped<IPatrocinadorRepository, PatrocinadorRepository>();

            services.AddScoped<IServiceBanco, ServiceBanco>();
            services.AddScoped<IBancoRepository, BancoRepository>();

            services.AddScoped<IServiceParceiro, ServiceParceiro>();
            services.AddScoped<IParceiroRepository, ParceiroRepository>();

            services.AddScoped<IServiceRamoAtividade, ServiceRamoAtividade>();
            services.AddScoped<IRamoAtividadeRepository, RamoAtividadeRepository>();

            services.AddScoped<IServiceCompra, ServiceCompra>();
            services.AddScoped<ICompraRepository, CompraRepository>();

            services.AddScoped<IServiceProduto, ServiceProduto>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IServicePlanoAssinatura, ServicePlanoAssinatura>();
            services.AddScoped<IPlanoAssinaturaRepository, PlanoAssinaturaRepository>();

            #endregion

            #region CookieAuth
            // configura autenticação por cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Usuario/LogOn/";
                });

            //Diz que para acessar qualquer action precisa está autenticado
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion CookieAuth

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc().AddMvcOptions(options => {
                options.Filters.AddService(typeof(GlobalFilter)); // Adiciona o middleware a todos os actions
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Definindo a cultura padrão: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Dashboard}/{id?}");
            });
        }
    }
}
