﻿using Microsoft.EntityFrameworkCore;
using NewApi.Context;
using NewApi.Models;

namespace NewApi.ApiEndPoints
{
    public static class ProdutosEndPoints
    {
        public static void MapProdutosEndPoints(this WebApplication app)
        {
            //------------------------------------Endpoints para Produtos----------------------------------




            app.MapPost("/produtos", async (Produto produto, AppDbContext db) => {

                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            });


            app.MapGet("/produtos", async (AppDbContext db) =>
                await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();


            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) => {

                return await db.Produtos.FindAsync(id)
                    is Produto produto
                        ? Results.Ok(produto)
                        : Results.NotFound("Id não encontrado, tente novamente.");
            });



            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) => {

                if (produto.ProdutoId != id)
                {
                    return Results.BadRequest("Dados informados não conferem, tente novamente.");
                }

                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null) return Results.NotFound("Este Id não existe, tente novamente para dar inicio a atualização do produto.");

                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                produtoDB.Imagem = produto.Imagem;
                produtoDB.DataCompra = produto.DataCompra;
                produtoDB.Estoque = produto.Estoque;
                produtoDB.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();
                return Results.Ok(produtoDB);

            });


            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) => {

                var produto = await db.Produtos.FindAsync(id);

                if (produto is null)
                {
                    return Results.NotFound("Este Id não existe, tente novamente.");
                }

                db.Produtos.Remove(produto);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });
        }
    }
}
