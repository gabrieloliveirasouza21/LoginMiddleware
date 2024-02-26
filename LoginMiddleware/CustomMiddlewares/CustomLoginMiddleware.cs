
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace LoginMiddleware.CustomMiddlewares
{
    public class CustomLoginMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string trueEmail = "gabriel@admin.com.br";
            string trueSenha = "admin123";
            StreamReader reader = new StreamReader(context.Request.Body);
            string queryString = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(queryString);

            if (queryDict.ContainsKey("email")
                && queryDict.ContainsKey("senha"))
            {
                var email = queryDict["email"].ToString();
                var senha = queryDict["senha"].ToString();

                if (email == trueEmail
                    && senha == trueSenha)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Login Correto");
                }
                else
                {
                    context.Response.StatusCode=401;
                    await context.Response.WriteAsync($"Email ou Senha incorretos");
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Insira o email e senha via query string no request body");
            }
        }
    }

    public static class LoginMiddlewareExtension
    {
        public static IApplicationBuilder UseLogin(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomLoginMiddleware>();
        }
    }
}
