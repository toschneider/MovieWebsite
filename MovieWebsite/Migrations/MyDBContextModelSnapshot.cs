﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieWebsite.DAL;

namespace MovieWebsite.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MovieWebsite.Models.Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MovieWebsite.Models.ActorMovie", b =>
                {
                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ActorID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("ActorMovies");
                });

            modelBuilder.Entity("MovieWebsite.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MovieWebsite.Models.GenreMovie", b =>
                {
                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("GenreID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("GenreMovies");
                });

            modelBuilder.Entity("MovieWebsite.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Format")
                        .HasColumnType("int");

                    b.Property<bool>("Original")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Owner")
                        .HasColumnType("int");

                    b.Property<int>("RegisseurID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Runtime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("RegisseurID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieWebsite.Models.Regisseur", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Regisseurs");
                });

            modelBuilder.Entity("MovieWebsite.Models.ActorMovie", b =>
                {
                    b.HasOne("MovieWebsite.Models.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieWebsite.Models.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieWebsite.Models.GenreMovie", b =>
                {
                    b.HasOne("MovieWebsite.Models.Genre", "Genre")
                        .WithMany("GenreMovies")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieWebsite.Models.Movie", "Movie")
                        .WithMany("GenreMovies")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieWebsite.Models.Movie", b =>
                {
                    b.HasOne("MovieWebsite.Models.Regisseur", "Regisseur")
                        .WithMany("Movies")
                        .HasForeignKey("RegisseurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
