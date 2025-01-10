using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestoSimplon.Class;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestoSimplonDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.useSwagger();
//    app.UseSwaggerUI(c =>
//    {
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestoSimplon API V1");
//    c.RoutePrefix = "";);
//}

app.UseHttpsRedirection();

RouteGroupBuilder RestoSimplon = app.MapGroup("/restoSimplon");
app.Run();

RestoSimplon.MapGet("/", GetClient);
RestoSimplon.MapGet("/", GetCategorie);
RestoSimplon.MapPost("/", CreateClient);
RestoSimplon.MapPut("/", UpdateClient);
RestoSimplon.MapDelete("/", DeleteClient);

static async Task<IResult> GetClient(int id, RestoSimplonDb db)
{
    return await db.Client.FindAsync(id)
        is Client Id
        ? TypedResults.Ok(id)
        : TypedResults.NotFound(); 
}
static async Task<IResult> GetCategorie(int id, RestoSimplonDb db)
{
    return await db.Categorie.FindAsync(id)
        is Categorie Id
        ? TypedResults.Ok(id)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateClient(Client client, RestoSimplonDb db)
{
    db.Client.Add(client);
    await db.SaveChangesAsync();



    return TypedResults.Created($"/client/{client.Id}");
        
}

static async Task<IResult> UpdateClient(int id, ClientDTO client, RestoSimplonDb db)
{
    var clientDb = await db.Client.FindAsync(id);
    if (clientDb is null) return TypedResults.NotFound();
    
    clientDb.Name = client.Name;
    clientDb.Prenom = client.Prenom;
    clientDb.Adress = client.Adress;
    clientDb.PhoneNumber = client.PhoneNumber;
    
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteClient(int id, RestoSimplon db)
{
    if (await db.Client.FindAsync(id) is Client client)
    {
        db.Client.Remove(client);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

//app.Mapget("/Client", GetClient) (async (int id, RestoSimplonDb db) =>
// await db.Clients.FindAsync(id))
//    is Client Id
//    ? TypedResults.Ok(client)
//    : TypedResults.NotFound(); 
