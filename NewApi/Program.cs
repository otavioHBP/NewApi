using NewApi.ApiEndPoints;
using NewApi.AppServicesExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAuthenticationJwt();


var app = builder.Build();

app.MapAutenticacaoEndPoints();

app.MapCategoriasEndPoints();

app.MapProdutosEndPoints();


var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();


app.UseAuthentication();
app.UseAuthorization();


app.Run();

