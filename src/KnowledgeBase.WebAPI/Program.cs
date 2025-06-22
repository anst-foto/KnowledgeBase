using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.Core;
using KnowledgeBase.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseName = builder.Configuration.GetConnectionString("DatabaseName");
var collectionName = builder.Configuration.GetConnectionString("CollectionName");
builder.Services.AddSingleton<IService>(new Service(new ConnectConfig(connectionString, databaseName, collectionName)));

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "v1"); });
}

app.UseCors();

const string baseUrl = "/api/v1";

var api = app.MapGroup($"{baseUrl}/articles")
    .WithOpenApi();

api.MapGet("/", (IService service) =>
    {
        var result = service.GetAllAsync();
        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    })
    .Produces<IAsyncEnumerable<Article>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

api.MapGet("/{id:guid}", async (Guid id, IService service) =>
    {
        var result = await service.GetByAsync(id);
        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    })
    .Produces<Article>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

api.MapGet("/tags", (HttpContext context, IService service) =>
    {
        var tags = context.Request.Query["tags"].ToList();
        var result = service.GetByAsync(tags);
        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    })
    .Produces<IAsyncEnumerable<Article>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

api.MapPost("/", async (Article article, IService service) =>
    {
        await service.CreateAsync(article);
        return Results.Created($"{baseUrl}/articles/{article.Id}", article);
    })
    .Produces<Article>(StatusCodes.Status201Created);

api.MapPut("/", async (Article article, IService service) =>
    {
        await service.UpdateAsync(article);

        return Results.Ok(article);
    })
    .Produces<Article>(StatusCodes.Status200OK);

api.MapDelete("/{id:guid}", async (Guid id, IService service) =>
    {
        await service.DeleteAsync(id);

        return Results.Ok();
    })
    .Produces(StatusCodes.Status200OK);

await app.RunAsync();
