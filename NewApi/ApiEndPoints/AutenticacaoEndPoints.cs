using Microsoft.AspNetCore.Authorization;
using NewApi.Models;
using NewApi.Services;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace NewApi.ApiEndPoints
{
    public static class AutenticacaoEndPoints
    {
        public static void MapAutenticacaoEndPoints(this WebApplication app)
        {
            //endpoint para login 
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login Invalido");
                }
                if (userModel.UserName == "otavio" && userModel.Password == "numsey")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt: Issuer"],
                        app.Configuration["Jwt: Audience"],
                        userModel);
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inválido");
                }

            }).Produces(StatusCodes.Status400BadRequest)
                                .Produces(StatusCodes.Status200OK)
                                .WithName("Login")
                                .WithTags("Autenticação");
        }
    }
}
