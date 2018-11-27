using System.IO;
using Medit.PMS.Application;
using Medit.PMS.Application.DepartmentApp;
using Medit.PMS.Application.MenuApp;
using Medit.PMS.Application.RoleApp;
using Medit.PMS.Application.UserApp;
using Medit.PMS.Domain.IRepositories;
using Medit.PMS.EFCore;
using Medit.PMS.EFCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Medit.PMS.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            PMSMapper.Initialize();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 获取数据库连接字符串
            var dbConnectionString = Configuration.GetConnectionString("PMSConnectionString");

            // 添加数据库上下文
            services.AddDbContext<PMSDbContext>(options => options.UseSqlServer(dbConnectionString, b => b.MigrationsAssembly("Medit.PMS.Web")));

            // 依赖注入 服务
            services.AddScoped<IUserAppService,UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleAppService, RoleAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IDepartmentAppService, DepartmentAppService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddMvc();

            // 添加Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
            });

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
