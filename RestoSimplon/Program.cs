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
            Name = "Valentin, Bafod� et Lisa",
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

//Ajouter les donn�es des articles dans la base de donn�es
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestoSimplonDB>();
    DbInitializer.Seed(dbContext);
}

RouteGroupBuilder restoSimplon = app.MapGroup("restoSimplon");

restoSimplon.MapGet("/", GetAllArticle)
    .WithMetadata(new SwaggerOperationAttribute(
         summary: "R�cup�re tous les �l�ments Articles",
         description: "Renvoie une liste de tous les �l�ments Articles"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des Articles"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment Articles a pu �tre trouver"));

restoSimplon.MapGet("/categorie", GetCategorie)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re un �l�ment Categorie par son ID",
        description: "Renvoie un/des �l�ments Articles par l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des articles en fonction de l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment n'a pu �tre trouver avec cette ID"));
restoSimplon.MapGet("/command", GetCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re un �l�ment Categorie par son ID",
        description: "Renvoie un/des �l�ments Articles par l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici la liste des articles en fonction de l'ID de la Categorie"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment n'a pu �tre trouver avec cette ID"));
restoSimplon.MapGet("/client/{id}", GetClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re un �l�ment Client par son ID",
        description: "Renvoie un �l�ment Client par l'ID du client"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Voici les informations du clients choisis"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment n'a pu �tre trouver avec cette ID"));
restoSimplon.MapPost("/", CreateClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�e un �l�ment Client",
        description: "Cr�e un �l�ment Client en suivant les informations donn�es"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre client a �tait cr�e avec succ�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Des �l�ments sont manquant ou le client est d�j� pr�sent"));
restoSimplon.MapPost("/command", CreateCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�e un �l�ment Command",
        description: "Cr�e un �l�ment Command en suivant les informations donn�es"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre commande a �tait cr�e avec succ�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Des �l�ments sont manquant ou la commande est d�j� en cours"));
restoSimplon.MapPut("/{id}", UpdateClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Met � jour un client par son ID",
        description: "Renvoie une modification par l'ID du client"))
    .WithMetadata(new SwaggerResponseAttribute(200, "La modification a �tait effecut� avec succ�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "La modification n'a pas pu �tre possible"));
restoSimplon.MapDelete("/client/{id}", DeleteClient)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Supprime un client par son ID",
        description: "Retire toute les informations d'un client par son ID"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre client a �t� retir� avec succ�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Le client n'a pas pu �tre retirer"));
restoSimplon.MapDelete("/command/{id}", DeleteCommand)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Supprime une commande par son ID",
        description: "Retire toute les informations d'une commande par son ID"))
    .WithMetadata(new SwaggerResponseAttribute(200, "Votre commande a �t� retir� avec succ�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "La commande n'a pas pu �tre retirer"));



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

