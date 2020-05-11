using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PWABlog.ControleDeAcesso;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;

namespace PWABlog
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
            // Adicionar o mecanismo do controle de acesso
            services.AddIdentity<Usuario, Papel>(options=>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<DatabaseContext>();

            // Adicionar o serviço do banco de dados
            services.AddDbContext<DatabaseContext>();
            
            // Adicionar os serviços de ORM das entidades do domínio"
            services.AddTransient<CategoriaOrmService>();
            services.AddTransient<PostagemOrmService>();
            services.AddTransient<AutorOrmService>();
            services.AddTransient<EtiquetaOrmService>();

            // Adicionar os serviços que possibilitam o funcionamento dos controllers e das views
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            // Configuração de Rotas 
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                */

                // Rotas da Área Comum
                endpoints.MapControllerRoute(
                    name: "comum",
                    pattern: "/",
                    defaults: new 
                    { 
                        controller = "Home",
                        action = "Index" 
                    }
                );

                // Rotas da Área Administrativa
                endpoints.MapControllerRoute(
                    name: "admin.categorias",
                    pattern: "admin/categorias/{action}/{id?}",
                    defaults: new { controller = "AdminCategorias", action = "Listar" }
                );

                endpoints.MapControllerRoute(
                      name: "admin.categorias",
                      pattern: "admin/categorias/{action}/{id?}",
                      defaults: new
                      {
                          controller = "AdminCategorias",
                          action = "Listar"
                      });

                endpoints.MapControllerRoute(
                   name: "admin.etiquetas",
                   pattern: "admin/etiquetas/{action}/{id?}",
                   defaults: new
                   {
                       controller = "AdminEtiquetas",
                       action = "Listar"
                   });

                endpoints.MapControllerRoute(
                   name: "admin.postagem",
                   pattern: "admin/postagem/{action}/{id?}",
                   defaults: new
                   {
                       controller = "AdminPostagem",
                       action = "Listar"
                   });
                endpoints.MapControllerRoute(
                   name: "admin.revisao",
                   pattern: "admin/postagem/revisao/{action}/{id?}",
                   defaults: new
                   {
                       controller = "AdminRevisao",
                       action = "Listar"
                   });
            });
        }
    }
}