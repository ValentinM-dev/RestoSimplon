using Microsoft.EntityFrameworkCore;
using RestoSimplon.Class;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<RestoSimplonDb>(opt => opt.UseSqlServer("RestoSimplonDb"));



var builder = WebApplication.CreateBuilder(args);

// Ajouter le service Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer Swagger pour qu'il soit utilisé dans l'application
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Active Swagger
    app.UseSwaggerUI(); // Active l'interface utilisateur de Swagger
}


app.Run();


var restoSimplon = app.MapGroup("/restoSimplon");


// Routes  articles:
restoSimplon.MapGet("/articles", GetAllArticle);  // Route pour obtenir tous les  articles
restoSimplon.MapGet("/article/{id}", GetArticle); // Route pour obtenir l'article par son id
restoSimplon.MapPost("/articles", CreateArticle);  // Route pour la création d'un article
restoSimplon.MapPut("/article/{id}", UpdateArticle); // Route pour la mise à jour d'un article
restoSimplon.MapDelete("/article/{id}", DeleteArticle); // Route pour supprimer un article


// Routes  commandes:
restoSimplon.MapGet("/commands", GetAllCommands); // Route pour obtenir toute les  commandes
restoSimplon.MapGet("/command/{id}", GetCommand); // Route pour la commande par son id 
restoSimplon.MapGet("/commands/bydate/{date}", GetCommandsByDate); //Route pour obtenir les commandes par date
restoSimplon.MapPost("/commands", CreateCommand); // Route pour la création d'une commande
restoSimplon.MapPut("/command/{id}", UpdateCommand); // Route pour la mise à jour d'une commande
restoSimplon.MapDelete("/command/{id}", DeleteCommand); // Route pour supprimer une commande




app.Run();



// Méthodes de gestion des routes articles 

static async Task<IResult> GetAllArticle(RestoSimplonDb db)
{
    var articles = await db.Articles.ToListAsync();  // la variable articles pour effectuer le requete a la bdd et recupérer les articles
    var articlesDTOs = articles.Select(article => new ArticleDTO(article)).ToList();
    return TypedResults.Ok(articlesDTOs);
}

static async Task<IResult> GetArticle(int id, RestoSimplonDb db)
{
    var article = await db.Articles.FindAsync(id);
    return article is not null
        ? TypedResults.Ok(new ArticleDTO(article))
        : TypedResults.NotFound();
}

static async Task<IResult> CreateArticle(ArticleDTO articleDTO, RestoSimplonDb db)
{
    var article = new Article
    {
        NameArticle = articleDTO.NameArticle,  // mise a jour des proprétés de l'article 
        CategorieId = articleDTO.CategorieId,
        PrixArticle = articleDTO.PrixArticle,
        Status = articleDTO.Status
    };

    db.Articles.Add(article);  // ajout en bdd
    await db.SaveChangesAsync(); // sauvegarde en bdd

    return TypedResults.Created($"/restoSimplon/articles/{article.Id}", articleDTO);
}

static async Task<IResult> UpdateArticle(int id, ArticleDTO articleDTO, RestoSimplonDb db)
{
    var article = await db.Articles.FindAsync(id);

    if (article is null)
        return TypedResults.NotFound();

    article.NameArticle = articleDTO.NameArticle;
    article.CategorieId = articleDTO.CategorieId;
    article.PrixArticle = articleDTO.PrixArticle;
    article.Status = articleDTO.Status;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteArticle(int id, RestoSimplonDb db)
{
    var article = await db.Articles.FindAsync(id);
    if (article is null)
        return TypedResults.NotFound();

    db.Articles.Remove(article);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}




// Méthode de gestion des routes pour commande

static async Task<IResult> GetAllCommands(RestoSimplonDb db)
{
    var commands = await db.Commands.ToListAsync();
    var commandsDTOs = commands.Select(command => new CommandDTO(command)).ToList();
    return TypedResults.Ok(commandsDTOs);
}


static async Task<IResult> GetCommand(int id, RestoSimplonDb db)
{
    var command = await db.Commands.FindAsync(id);
    return command is not null
        ? TypedResults.Ok(new CommandDTO(command))
        : TypedResults.NotFound();
}



static async Task<IResult> CreateCommand(CommandDTO commandDTO, RestoSimplonDb db)
{
    var command = new Command
    {
        Id = commandDTO.Id,
        ClientId = commandDTO.ClientId,
        MontantCommande = commandDTO.MontantCommande,
        DateCommand = commandDTO.DateCommand,
        ArticleList = commandDTO.ArticleList,
        NbArticle = commandDTO.NbArticle
    };

    db.Commands.Add(command);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/commands/{command.Id}", new CommandDTO
    {
        Id = command.Id,
        ClientId = command.ClientId,
        MontantCommande = command.MontantCommande,
        DateCommand = command.DateCommand,
        ArticleList = command.ArticleList,
        NbArticle = command.NbArticle
    });
}


static async Task<IResult> UpdateCommand(int id, CommandDTO commandDTO, RestoSimplonDb db)
{
    var command = await db.Commands
        .Include(c => c.ArticleList) // Inclure la liste des articles
        .FirstOrDefaultAsync(c => c.Id == id);

    if (command is null)
        return TypedResults.NotFound();

    var articles = await db.Articles
        .Where(article => commandDTO.ArticleList.Contains(article.Id.ToString()))    // Récupérer les articles mis à jour
        .ToListAsync();

  //command.Id = commandDTO.Id;
    command.ClientId = commandDTO.ClientId;
    command.DateCommand = commandDTO.DateCommand;
    command.MontantCommande = commandDTO.MontantCommande;
    command.ArticleList = articles; // Mettre à jour la liste des articles
    command.NbArticle = commandDTO.NbArticle;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}



static async Task<IResult> DeleteCommand(int id, RestoSimplonDb db)
{
    var command = await db.Commands.FindAsync(id); // recherche de la commande dans la bdd

    if (command is null)
        return TypedResults.NotFound();

    db.Commands.Remove(command);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> GetCommandsByDate(DateTime date, RestoSimplonDb db)
{
    var commands = await db.Commands
        .Where(c => c.DateCommand.Date == date.Date)  // trie des commandes par date
        .ToListAsync();

    var commandsDTOs = commands.Select(command => new CommandDTO(command)).ToList(); // genère la liste de commandes a la date voulue

    return commandsDTOs.Any()
        ? TypedResults.Ok(commandsDTOs)
        : TypedResults.NotFound();
}





