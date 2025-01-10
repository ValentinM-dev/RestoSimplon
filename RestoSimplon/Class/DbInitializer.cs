using System.Text.Json;

namespace RestoSimplon.Class
{
    public class DbInitializer
    {
        public static void Seed(RestoSimplonDB context)
        {
            //Charger et insérer les données depuis un fichier JSON
            if (!context.Clients.Any())
            {
                string articlesJsonPath = "articles.json"; //Chemin du .json
                string articlesJsonContent = File.ReadAllText(articlesJsonPath);

                // Désérialisation du fichier JSON en liste d'articles
                List<Article> articles = JsonSerializer.Deserialize<List<Article>>(articlesJsonContent);

                //Ajout des articles dans la base de donnée
                if(articles != null )
                {
                    context.Articles.AddRange(articles);
                }
            }
        }
    }
}
