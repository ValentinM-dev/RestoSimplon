using Microsoft.EntityFrameworkCore;

namespace RestoSimplon.Class
{
    public class RestoSimplonDB : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Création de la table Client
            modelBuilder.Entity<Client>();


            // Création de la table Commande
            modelBuilder.Entity<Command>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(c => c.ClientId); // Relation avec Client



            //Création de la table Article
            modelBuilder.Entity<Article>()
                        .HasOne<Categorie>()
                        .WithOne()
                        .HasForeignKey(a => a.CategorieId);
        }
    }
}
