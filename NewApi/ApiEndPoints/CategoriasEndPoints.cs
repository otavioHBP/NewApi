using Microsoft.EntityFrameworkCore;
using NewApi.Context;
using NewApi.Models;
using System.Runtime.CompilerServices;

namespace NewApi.ApiEndPoints
{
    public static class CategoriasEndPoints
    {
        public static void  MapCategoriasEndPoints(this WebApplication app)
        {
            //------------------------------------Endpoints para Categorias----------------------------------

            app.MapGet("/", () => "Produtos - 2023").ExcludeFromDescription();


            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) => {

                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
            });


            app.MapGet("/categorias", async (AppDbContext db) =>
                await db.Categorias.ToListAsync()).WithTags("Categorias").RequireAuthorization();


            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) => {
                return await db.Categorias.FindAsync(id)
                    is Categoria categoria
                        ? Results.Ok(categoria)
                        : Results.NotFound("Id não encontrado, tente novamente.");
            });



            app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, AppDbContext db) => {
                if (categoria.CategoriaId != id)
                {
                    return Results.BadRequest("Dados informados não conferem, tente novamente.");
                }

                var categoriaDB = await db.Categorias.FindAsync(id);

                if (categoriaDB is null) return Results.NotFound("Este Id não existe, tente novamente para dar inicio a atualização da Categoria.");

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(categoriaDB);

            });


            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) => {

                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                {
                    return Results.NotFound("Este Id não existe, tente novamente.");
                }

                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });

        }
    }
}
