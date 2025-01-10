using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Annotations;
using RestoSimplon.Class;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using Microsoft.VisualBasic;
using System.Text.Json;
using System;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestoSimplonDB>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v0.1", new OpenApiInfo
    {
        Title = "RestoSimplon Api",
        Version = "v0.1",
        Description = "Version 0.1 de l'API pour gerer des Commandes, Articles et Clients",
        Contact = new OpenApiContact
        {
            Name = "Valentin, Bafodé et Lisa",
            Email = "RestoSimplon@exercice.com",
            Url = new Uri("https://RestoSimpon.com"),
        }
    });

    // Annotations Swagger
    c.EnableAnnotations();
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "RestoSimplon API V0.1");
        c.RoutePrefix = "";
    });
}

//Ajouter les données des articles dans la base de données
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestoSimplonDB>();
    DbInitializer.Seed(dbContext);
}

RouteGroupBuilder restoSimplon = app.MapGroup("restoSimplon");

restoSimplon.MapGet("/", GetAllArticle)
    .WithMetadata(new SwaggerOperationAttribute(
         summary: "Récupère tous les éléments Articles",
         description: "Renvoie une liste de tous les éléments Articles"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des Articles"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément Articles a pu être trouver"));

restoSimplon.MapGet("/categorie", GetCategorie)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère un élément Categorie par son ID",
        description: "Renvoie un/des éléments Articles par l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des articles en fonction de l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément n'a pu être trouver avec cette ID"));
restoSimplon.MapGet("/command", GetCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère un élément Categorie par son ID",
        description: "Renvoie un/des éléments Articles par l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des articles en fonction de l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément n'a pu être trouver avec cette ID"));
restoSimplon.MapGet("/client/{id}", GetClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère un élément Client par son ID",
        description: "Renvoie un élément Client par l'ID du client"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici les informations du clients choisis"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément n'a pu être trouver avec cette ID"));
restoSimplon.MapPost("/", CreateClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Crée un élément Client",
        description: "Crée un élément Client en suivant les informations données"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre client a était crée avec succès"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Des éléments sont manquant ou le client est déjà présent"));
restoSimplon.MapPost("/command", CreateCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Crée un élément Command",
        description: "Crée un élément Command en suivant les informations données"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre commande a était crée avec succès"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Des éléments sont manquant ou la commande est déjà en cours"));
restoSimplon.MapPut("/{id}", UpdateClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Met à jour un client par son ID",
        description: "Renvoie une modification par l'ID du client"))
    .WithMetadata(new SwaggerResponseAttribute(200, "La modification a était effecuté avec succès"))
    .WithMetadata(new SwaggerResponseAttribute(404, "La modification n'a pas pu être possible"));
restoSimplon.MapDelete("/client/{id}", DeleteClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Supprime un client par son ID",
        description: "Retire toute les informations d'un client par son ID"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre client a été retiré avec succès"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Le client n'a pas pu être retirer"));
restoSimplon.MapDelete("/command/{id}", DeleteCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Supprime une commande par son ID",
        description: "Retire toute les informations d'une commande par son ID"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre commande a été retiré avec succès"))
    .WithMetadata(new SwaggerResponseAttribute(404, "La commande n'a pas pu être retirer"));



app.Run();

static async Task<IResult> GetAllArticle(RestoSimplonDB db) => TypedResults.Ok(await db.Articles.ToArrayAsync());
static async Task<IResult> GetCategorie(int id, RestoSimplonDB db)
{
    return await db.Categories.FindAsync(id)
        is Categorie Id
        ? TypedResults.Ok(Id)
        : TypedResults.NotFound();
}
static async Task<IResult> GetCommand(int id, RestoSimplonDB db)
{
    return await db.Commands.FindAsync(id)
        is Command Id
        ? TypedResults.Ok(Id)
        : TypedResults.NotFound();
}
static async Task<IResult> GetClient(int id, RestoSimplonDB db)
{
    return await db.Clients.FindAsync(id)
        is Client Id
        ? TypedResults.Ok(Id)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateClient(Client Id, RestoSimplonDB db)
{
    db.Clients.Add(Id);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/client/{Id.Id}", Id);
}

static async Task<IResult> CreateCommand(Command Id, RestoSimplonDB db)
{
    db.Commands.Add(Id);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/command/{Id.Id}", Id);
}

static async Task<IResult> UpdateClient(int id, Client client, RestoSimplonDB db)
{
    var Id = await db.Clients.FindAsync(id);

    if (Id is null) return TypedResults.NotFound();

    Id.Name = client.Name;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteClient(int id, RestoSimplonDB db)
{
    if (await db.Clients.FindAsync(id) is Client Id)
    {
        db.Clients.Remove(Id);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

static async Task<IResult> DeleteCommand(int id, RestoSimplonDB db)
{
    if (await db.Commands.FindAsync(id) is Command Id)
    {
        db.Commands.Remove(Id);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

