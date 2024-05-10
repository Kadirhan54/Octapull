using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Octapull.Domain.Identity;
using Octapull.MVC.Controllers;
using Octapull.Persistence.Contexts.Application;
using System.Text;

namespace Octapull.MVC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddSession();

            // Add services for SignInManager
            services.AddScoped<SignInManager<ApplicationUser>>();

            services.AddHttpClient<MeetingController>();

            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                // User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@$";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(30);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Admin"));
            });

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var jwtSecret = configuration.GetSection("JWTSecret").Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = new PathString("/Auth/Login");
            //    options.LogoutPath = new PathString("/Auth/Logout");
            //    options.Cookie = new CookieBuilder
            //    {
            //        Name = "YetgenJump",
            //        HttpOnly = true,
            //        SameSite = SameSiteMode.Strict,
            //        SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
            //    };
            //    options.SlidingExpiration = true;
            //    options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
            //    options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            //});

            //// Create roles during application startup
            //using (var serviceProvider = services.BuildServiceProvider())
            //{
            //    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>(); // Use your custom Role class
            //    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            //    // Check if roles exist, and create them if not
            //    if (!await roleManager.RoleExistsAsync("Admin"))
            //    {
            //        await roleManager.CreateAsync(new Role { Name = "Admin" }); // Use your custom Role class
            //    }

            //    var user = await userManager.FindByEmailAsync("admin@example.com");
            //    await userManager.AddToRoleAsync(user, "Admin");
            //}

            return services;
        }

    }
}
