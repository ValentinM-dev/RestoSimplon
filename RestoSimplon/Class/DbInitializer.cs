using System.Text.Json;

namespace RestoSimplon.Class
{
    public class DbInitializer
    {

        public class ArticleJson
        {
            public string Nom { get; set; }
            public string Categorie { get; set; }
            public double Prix { get; set; }
        }

        public class ClientJson
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Adresse { get; set; }
            public string Phone { get; set; }
        }

        public static void Seed(RestoSimplonDB context)
        {
            

            //Charger et insérer les données depuis un fichier JSON 
            if (!context.Articles.Any())
            {
                string articlesJsonPath = "JSON/articles.json"; //Chemin du .json
                string articlesJsonContent = File.ReadAllText(articlesJsonPath);

                // Désérialisation du fichier JSON en liste d'articles
                List<ArticleJson> articles = JsonSerializer.Deserialize<List<ArticleJson>>(articlesJsonContent);

                //Ajout des articles dans la base de donnée
                if(articles != null )
                {
                    context.Articles.AddRange(articles.Select(article => new Article
                    {
                        NameArticle = article.Nom,
                        PrixArticle = article.Prix,
                        CategorieId = article.Categorie switch
                        {
                            "Entrée" => 1,
                            "Plat" => 2,
                            "Dessert" => 3,
                            _ => throw new Exception("Categorie Inconnue")
                        },
                        Status = true
                    }));
                }
            }

            //Charger et insérer les données depuis un fichier JSON 
            if (!context.Clients.Any())
            {
                string clientsJsonPath = "JSON/clients.json"; //Chemin du .json
                string clientsJsonContent = File.ReadAllText(clientsJsonPath);

                // Désérialisation du fichier JSON en liste d'articles
                List<ClientJson> client = JsonSerializer.Deserialize<List<ClientJson>>(clientsJsonContent);

                //Ajout des articles dans la base de donnée
                if (client != null)
                {
                    context.Clients.AddRange(client.Select(article => new Client
                    {
                        Name = article.Nom,
                        Prenom = article.Prenom,
                        Adress = article.Adresse,
                        PhoneNumber =  article.Phone,
                    }));
                }
            }

            //Charger les catégories de manière Statique
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Categorie { Name = "Entrée" },
                    new Categorie { Name = "Plat" },
                    new Categorie { Name = "Dessert" }
                );
            }

            context.SaveChanges();
        }
    }
}
