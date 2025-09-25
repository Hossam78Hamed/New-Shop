using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Product;
using Shop.Core.Mapper.CategoryMapper;
using Shop.Core.Mapper.ProductMappers;
using Shop.Core.Services.CategoryServices;
using Shop.infrastructure;
using System.Reflection;
using AutoMapper;
using Shop.API.Middleware;
namespace Shop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            /////------------

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
            //AddInfrastructure
            builder.Services.AddInfrastructure(builder.Configuration);





            /////------------------
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            ///-------------
            //MyCors
            app.UseCors("MyCors");
            //ExceptionsMiddleware
            app.UseMiddleware<ExceptionsMiddleware>();//global exception handling //ÂÌ—ÊÕ · «·’›ÕÂ (ExceptionsMiddleware) // œÏ ﬁ»· ﬂ· —Êﬂ”  

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            ////if you write un correct api well go to this rout
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");//{0}=statuesCode
           



            ///-------------
            app.UseHttpsRedirection();

         
            

            app.MapControllers();

            app.Run();
        }
    }
}
