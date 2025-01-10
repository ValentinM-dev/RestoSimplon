using Swashbuckle.AspNetCore.Annotations;

namespace RestoSimplon.Class
{
    public class Command
    {
        public int Id { get; set; }
        public required int ClientId { get; set; }
        public required int MontantCommande { get; set; }
        public required DateTime DateCommand { get; set; }
        public List<Article> Articles { get; } = [];
        public required int NbArticle { get; set; }
    }

    public class CommandDTO
    {
        [SwaggerSchema("Identifiant unique de l'article")]
        public int Id { get; set; }

        [SwaggerSchema("Récupération de l'ID du client")]
        public required int ClientId { get; set; }

        [SwaggerSchema("Montant de la commande")]
        public required int MontantCommande { get; set; }

        [SwaggerSchema("Date de la commande")]
        public required DateTime DateCommand { get; set; }

        [SwaggerSchema("Liste des Articles dans la commande")]
        public List<Article> Articles { get; } = [];

        [SwaggerSchema("Nombre d'article total")]
        public required int NbArticle { get; set; }


        public CommandDTO() { }

        public CommandDTO(Command commande)
        {
            Id = commande.Id;
            ClientId = commande.ClientId;
            MontantCommande = commande.MontantCommande;
            DateCommand = commande.DateCommand;
            Articles = commande.Articles;
            NbArticle = commande.NbArticle;
        }
    }
}
