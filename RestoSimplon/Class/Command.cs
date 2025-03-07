﻿namespace RestoSimplon.Class
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
        public int Id { get; set; }
        public required int ClientId { get; set; }
        public required int MontantCommande { get; set; }
        public required DateTime DateCommand { get; set; }
        public List<Article> Articles { get; } = [];
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
