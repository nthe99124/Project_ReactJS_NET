using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class LoggerMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}
