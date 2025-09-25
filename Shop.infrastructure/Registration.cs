using Microsoft.Extensions.DependencyInjection;
using Shop.Core.interfaces;
using Shop.infrastructure.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Shop.infrastructure.Data;
using Shop.Core.Services.CategoryServices;
using Shop.Core.Mapper.CategoryMapper;
using Shop.Core.Services.ProductServices;
using Shop.Core.Services.PhotoServices;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Configuration;
using Shop.Core.Mapper.ProductMappers;
using Shop.Core.Mapper.PhotoMappers;
using StackExchange.Redis;
using Shop.Core.Services.CustomerBasketServices;
using System.Runtime.InteropServices;
using Shop.Core.Services.AccountServices;
using Shop.Core.Services.EmailServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
///JWT
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Accounts;
using Shop.Core.Services.OrderServices;
using Shop.Core.Services.DeliveryMethodServices;
using System.Text.Json.Serialization;
using Shop.Core.Services.AddressServices;
using Shop.Core.Services.PaymentServices;
namespace Shop.infrastructure
{
    public static class Registration

    {
        public static  IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            ////becouse IGenericRepository is generic
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            #region Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            #endregion

            #region Photo
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            ///إضافة IFileProvider كخدمة
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
           
            #endregion

            #region  Category
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();


            #endregion

            #region CustomerBasket
            services.AddScoped<ICustomerBasketService, CustomerBasketService>();
            services.AddScoped<ICustomerBasketRepository, CustomerBasketRepository>();

            #endregion



            #region AccountService
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGenerateToken, GenerateToken>();



            #endregion


            #region  Order


            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            #endregion


            #region  DeliveryMethod


            services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();
            services.AddScoped<IDeliveryMethodService, DeliveryMethodService>();
            #endregion


            #region Address

            services.AddScoped<IAddressService,AddressService > ();
            services.AddScoped<IAddressRepository, AddressRepository>();


            #endregion

            #region PaymentService
            services.AddScoped<IPaymentService, PaymentService>();
            #endregion




            #region DB
            //services.AddDbContext<AppDbContext>(op =>
            //{
            //    op.UseSqlServer(configuration.GetConnectionString("ShopDB")).UseLazyLoadingProxies();

            //    op.UseSqlServer();
            //});



            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ShopDB"),
                    b => b.MigrationsAssembly("Shop.infrastructure")).UseLazyLoadingProxies();

            });



            #endregion

            #region Redis
            //apply Redis Connectoon
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });
            #endregion



            #region AddAutoMapper
            // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //
            //builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CategoryMapper).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ProductMapper).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(PhotoMapper).Assembly));

            #endregion




           
            #region JWT

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(x =>
            {
                x.Cookie.Name = "token";
                x.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {

                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["token"];
                        context.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });


            #endregion



            #region instead of  [JsonIgnore]
            // Add controllers with settings
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true; // optional, pretty-print JSON
                });

            #endregion


            return services;

        }

    }
}

#region  How This Works Together (code of AddAuthentication )
//When a user logs in:

//You generate a JWT.

//Store it in a cookie called "token".

//On each request:

//The browser sends the "token" cookie automatically.

//OnMessageReceived grabs the token from the cookie and sets it for JWT validation.

//ASP.NET Core validates:

//Signature using Token:Secret.

//Issuer(Token: Issuer).

//Expiration and other claims.

//If valid:

//User is authenticated, and HttpContext.User is populated with the claims.

//If invalid:

//Returns 401 Unauthorized.
#endregion

