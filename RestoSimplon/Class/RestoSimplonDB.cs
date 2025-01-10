﻿using Microsoft.EntityFrameworkCore;

namespace RestoSimplon.Class
{
    public class RestoSimplonDB : DbContext
    {
        public RestoSimplonDB(DbContextOptions<RestoSimplonDB> options)
        : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<ListArticles> ListArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Création de la table Client
            modelBuilder.Entity<Client>();


            // Création de la table Commande
            modelBuilder.Entity<Command>()
                .HasMany(c => c.Articles)
                .WithMany(c => c.Commands)
                .UsingEntity<ListArticles>();



            //Création de la table Article
            modelBuilder.Entity<Article>()
                        .HasOne<Categorie>() // Relation avec Categorie
                        .WithMany()
                        .HasForeignKey(a => a.CategorieId); // Clé Etrangère CategorieId
                        


        }
    }
}
