using LearnCore.Application.UserApp;
using LearnCore.Domain.IRepositories;
using LearnCore.EntityFrameworkCore;
using LearnCore.EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LearnCore.MVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");

            //添加数据上下文
            services.AddDbContext<LearnCoreDbContext>(options => options.UseNpgsql(sqlConnectionString));
            //services.AddDbContext<LearnCoreDbContext>(options => options.UseNpgsql(sqlConnectionString), ServiceLifetime.Transient);  //Transient     ServiceProvider总是创建一个新的服务实例。
            //services.AddDbContext<LearnCoreDbContext>(options => options.UseNpgsql(sqlConnectionString), ServiceLifetime.Scoped);     //Scoped        ServiceProvider创建的服务实例由自己保存，（同一次请求）所以同一个ServiceProvider对象提供的服务实例均是同一个对象。
            //services.AddDbContext<LearnCoreDbContext>(options => options.UseNpgsql(sqlConnectionString), ServiceLifetime.Singleton);  //Singleton     始终是同一个实例对象
            //添加依赖注入
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();

            // Add framework services.
            services.AddMvc();
            // Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //开发环境异常处理
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                //生产环境异常处理
                app.UseExceptionHandler("/Shared/Error");
            }

            //使用静态文件
            app.UseStaticFiles();
            //请求管道中启用Session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });

            //SendData.Initialize(app.ApplicationServices); //初始化数据
        }
    }
}
