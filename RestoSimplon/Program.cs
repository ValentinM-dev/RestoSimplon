using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Annotations;
using RestoSimplon.Class;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using Microsoft.VisualBasic;

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
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestoSimplon API V0.1");
        c.RoutePrefix = "";
    });
}

RouteGroupBuilder restoSimplon = app.MapGroup("restoSimplon");

restoSimplon.MapGet("/", GetAllArticle); 
restoSimplon.MapGet("/categorie", GetCategorie); 
restoSimplon.MapGet("/command", GetCommand);
restoSimplon.MapGet("/{id}", GetClient);
restoSimplon.MapPost("/", CreateClient);
restoSimplon.MapPost("/command", CreateCommand);
restoSimplon.MapPut("/{id}", UpdateClient);
restoSimplon.MapDelete("/{id}", DeleteClient);
restoSimplon.MapDelete("/{id}", DeleteCommand);

app.Run();

static async Task<IResult> GetAllArticle(RestoSimplonDB db) => TypedResults.Ok(await db.Articles.ToArrayAsync());
static async Task<IResult> GetCategorie(int id,  RestoSimplonDB db)
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

static async Task<IResult> UpdateClient( int id, Client client, RestoSimplonDB db)
{
    var Id = await db.Clients.FindAsync(id);

    if (Id is null) return TypedResults.NotFound();

    Id.Name = client.Name;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteClient(int id, RestoSimplonDB db)
{
    if(await db.Clients.FindAsync(id) is Client Id)
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
