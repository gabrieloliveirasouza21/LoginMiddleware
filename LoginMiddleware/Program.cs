using LoginMiddleware.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomLoginMiddleware>();
var app = builder.Build();

app.UseLogin();

app.Run();
