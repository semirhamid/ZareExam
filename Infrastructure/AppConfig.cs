

using System.Text;
using ZareExam.BusinessLogic;
using ZareExam.Infrastructure;
using ZareExam.Infrastructure.Db;
using ZareExam.Interface;
using ZareExam.Models.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ZareExam.Infrastructure

{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddDbContext<AuthDbContext>(options =>
                            options.UseSqlServer(
                                configuration.GetConnectionString("DefaultConnection")));
            
            // setup dependency injections
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<AuthDbContext>();
            services.AddScoped<AppDbSeeder>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IStudentInterface, StudentManager>();
            services.AddScoped<IDepartmentInterface, DepartmentManager>();
            

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

             // create identity service using AppUser class and Role
            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<AuthDbContext>();


            // jwt key
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]!);

            // setup JWT authentication policy
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameters;
            });
            // enable end point explorer for Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}