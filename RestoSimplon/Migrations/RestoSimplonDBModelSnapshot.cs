﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoSimplon.Class;

#nullable disable

namespace RestoSimplon.Migrations
{
    [DbContext(typeof(RestoSimplonDB))]
    partial class RestoSimplonDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("RestoSimplon.Class.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategorieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameArticle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("PrixArticle")
                        .HasColumnType("REAL");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RestoSimplon.Class.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RestoSimplon.Class.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RestoSimplon.Class.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCommand")
                        .HasColumnType("TEXT");

                    b.Property<int>("MontantCommande")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NbArticle")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("RestoSimplon.Class.ListArticles", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommandId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticleId", "CommandId");

                    b.HasIndex("CommandId");

                    b.ToTable("ListArticles");
                });

            modelBuilder.Entity("RestoSimplon.Class.Article", b =>
                {
                    b.HasOne("RestoSimplon.Class.Categorie", null)
                        .WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestoSimplon.Class.ListArticles", b =>
                {
                    b.HasOne("RestoSimplon.Class.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestoSimplon.Class.Command", null)
                        .WithMany()
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
