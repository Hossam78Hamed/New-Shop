using Microsoft.Extensions.Caching.Memory;
using Shop.API.Helper;
using System.Net;
using System.Text.Json;

namespace Shop.API.Middleware
{
    public class ExceptionsMiddleware
    //نستخدم
    // middleware to handle exceptions globally
    {
        private readonly RequestDelegate next;// to call the next middleware in the pipeline    
        private readonly IHostEnvironment environment;//to know if we are in development or production  

        private readonly IMemoryCache memoryCache;  
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(5);
        
        public ExceptionsMiddleware(RequestDelegate _next, IHostEnvironment _environment,IMemoryCache _memoryCache)
        {
            next = _next;
            environment= _environment;
            memoryCache= _memoryCache;
        }
            public async Task InvokeAsync(HttpContext context)
            {

                try {
                ApplySecurity(context);
                if (!IsRequestAllowed(context))
                {
                    if (!context.Response.HasStarted) // ✅ prevent invalid operation
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                        context.Response.ContentType = "application/json";

                        var response = new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later"); 
                        

                        await context.Response.WriteAsJsonAsync(response);
                        return; // stop pipeline
                    }
                }
                await next(context);



            }

            catch (Exception ex) {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                //لو فى مرحله التطوير يعمل رساله بتفاصيل  اقل
                var response = environment.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                //var response =
                //    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
                
                }
            
            }
        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cachKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            if (!memoryCache.TryGetValue(cachKey, out (DateTime timestamp, int count) cacheEntry))
            {
                // If nothing in cache → create new entry
                cacheEntry = (dateNow, 0);
                memoryCache.Set(cachKey, cacheEntry, _rateLimitWindow);
            }

            var (timesTamp, count) = cacheEntry;
            if (dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 5)
                {
                    return false;
                }
                memoryCache.Set(cachKey, (timesTamp, count += 1), _rateLimitWindow);
            }
            else
            {
                memoryCache.Set(cachKey, (dateNow, count), _rateLimitWindow);
            }
            return true;
        }
        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";

        }
    }

    }

