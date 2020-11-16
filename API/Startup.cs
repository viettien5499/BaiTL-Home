using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Helper;
using DAL.Interfaces;
using Helper;
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

namespace API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
            services.AddTransient<IDatabaseHelper, DatabaseHelper>();
            services.AddTransient<IItemGroupRepository, ItemGroupRepository>();
            services.AddTransient<IItemGroupBusiness, ItemGroupBusiness>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemBusiness, ItemBusiness>();
            services.AddTransient<ISanPhamRepository, SanPhamRepository>();
            services.AddTransient<ISanPhamBusiness, SanPhamBusiness>();
            services.AddTransient<IKhachHangRepository, KhachHangRepository>();
            services.AddTransient<IKhachHangBusiness, KhachHangBusiness>();
            services.AddTransient<ILoaiSanPhamRepository, LoaiSanPhamRepository>();
            services.AddTransient<ILoaiSanPhamBusiness, LoaiSanPhamBusiness>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerBusiness, CustomerBusiness>();
            services.AddTransient<IHoaDonBanRepository, HoaDonBanRepository>();
            services.AddTransient<IHoaDonBanBusiness, HoaDonBanBusiness>();
            //services.AddTransient<ICTHoaDonBanRepository, CTHoaDonBanRepository>();
            //services.AddTransient<ICTHoaDonBanBusiness, CTHoaDonBanBusiness>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INewsBusiness, NewsBusiness>();
            services.AddTransient<INewsRepository, NewsReponsitory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            // global cors policy
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