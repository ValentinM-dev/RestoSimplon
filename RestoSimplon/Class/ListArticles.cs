using Swashbuckle.AspNetCore.Annotations;

namespace RestoSimplon.Class
{
    public class ListArticles
    {
        public required int ArticleId { get; set; }
        public required int CommandId { get; set; }
    }

    public class ListArticlesDTO
    {
        [SwaggerSchema("Récupération de l'ID de l'article")]
        public required int ArticleId { get; set; }

        [SwaggerSchema("Récupération de l'ID de la commande")]
        public required int CommandId { get; set; }

        public ListArticlesDTO() { }
        public ListArticlesDTO(ListArticles listArticles)
        {
            ArticleId = listArticles.ArticleId;
            CommandId = listArticles.CommandId;
        }
    }
}
