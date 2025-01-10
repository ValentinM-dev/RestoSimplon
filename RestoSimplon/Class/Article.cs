using Swashbuckle.AspNetCore.Annotations;

namespace RestoSimplon.Class
{
    public class Article
    {
        public int Id { get; set; }
        public required string NameArticle { get; set; }
        public required int CategorieId { get; set; }
        public required float PrixArticle { get; set; }
        public required bool Status { get; set; }
        public List<Command> Commands { get; } = [];
    }

    public class ArticleDTO
    {
        [SwaggerSchema("Identifiant unique de l'article")]
        public int Id { get; set; }

        [SwaggerSchema("Nom de l'Article")]
        public required string NameArticle { get; set; }

        [SwaggerSchema("Identifiant Ratacher à la Catégorie")]
        public required int CategorieId { get; set; }

        [SwaggerSchema("Prix de l'Article")]
        public required float PrixArticle { get; set; }

        [SwaggerSchema("Status de l'élément (Présent ou en Rupture de stock")]
        public required bool Status { get; set; }

        [SwaggerSchema("Récupération de la liste des Commandes sur l'Article")]
        public List<Command> Commands { get; } = [];

        public ArticleDTO() { }

        public ArticleDTO(Article article)
        {
            Id = article.Id;
            NameArticle = article.NameArticle;
            CategorieId = article.CategorieId;
            PrixArticle = article.PrixArticle;
            Status = article.Status;
            Commands = article.Commands;
        }
    }
}
