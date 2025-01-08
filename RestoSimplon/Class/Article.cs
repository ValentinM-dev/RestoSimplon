namespace RestoSimplon.Class
{
    public class Article
    {
        public int Id { get; set; }
        public required string NameArticle { get; set; }
        public required int CategorieId { get; set; }
        public required float PrixArticle { get; set; }
        public required bool Status { get; set; }
    }

    public class ArticleDTO
    {
        public int Id { get; set; }
        public required string NameArticle { get; set; }
        public required int CategorieId { get; set; }
        public required float PrixArticle { get; set; }
        public required bool Status { get; set; }

        public ArticleDTO() { }

        public ArticleDTO(Article articles)
        {
            Id = articles.Id;
            NameArticle = articles.NameArticle;
            CategorieId = articles.CategorieId;
            PrixArticle = articles.PrixArticle;
            Status = articles.Status;
        }
    }
}
