﻿// <auto-generated />
using System;
using ApiAspNet.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiAspNet.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250621022510_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiAspNet.Entities.Agence", b =>
                {
                    b.Property<int>("IdAgence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdAgence"));

                    b.Property<string>("AdresseAgence")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int?>("IdGestionnaire")
                        .HasColumnType("integer");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<string>("NineaGestionnaire")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("RccmGestionnaire")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("IdAgence");

                    b.HasIndex("IdGestionnaire");

                    b.ToTable("Agences");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Chauffeur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.ToTable("Chauffeurs");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Flotte", b =>
                {
                    b.Property<int>("IdFlotte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdFlotte"));

                    b.Property<string>("MatriculeFlotte")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("TypeFlotte")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("IdFlotte");

                    b.ToTable("Flottes");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Offre", b =>
                {
                    b.Property<int>("IdOffre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdOffre"));

                    b.Property<string>("DescriptionOffre")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("DisponibiliteOffre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("IdAgence")
                        .HasColumnType("integer");

                    b.Property<float>("PrixOffre")
                        .HasColumnType("real");

                    b.HasKey("IdOffre");

                    b.HasIndex("IdAgence");

                    b.ToTable("Offre");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdReservation"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("MontantReservation")
                        .HasColumnType("real");

                    b.Property<string>("StatutReservation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdReservation");

                    b.HasIndex("ClientId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ApiAspNet.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ApiAspNet.Entities.Voyage", b =>
                {
                    b.Property<int>("IdVoyage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVoyage"));

                    b.Property<DateTime>("DateArrivee")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateDepart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("OffreIdOffre")
                        .HasColumnType("integer");

                    b.Property<float>("Prix")
                        .HasColumnType("real");

                    b.HasKey("IdVoyage");

                    b.HasIndex("OffreIdOffre");

                    b.ToTable("Voyage");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Client", b =>
                {
                    b.HasBaseType("ApiAspNet.Entities.User");

                    b.Property<string>("CniClient")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Gestionnaire", b =>
                {
                    b.HasBaseType("ApiAspNet.Entities.User");

                    b.Property<string>("CNIGestionnaire")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasDiscriminator().HasValue("Gestionnaire");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Agence", b =>
                {
                    b.HasOne("ApiAspNet.Entities.Gestionnaire", "Gestionnaire")
                        .WithMany("Agences")
                        .HasForeignKey("IdGestionnaire");

                    b.Navigation("Gestionnaire");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Offre", b =>
                {
                    b.HasOne("ApiAspNet.Entities.Agence", "Agence")
                        .WithMany("Offres")
                        .HasForeignKey("IdAgence")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agence");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Reservation", b =>
                {
                    b.HasOne("ApiAspNet.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Voyage", b =>
                {
                    b.HasOne("ApiAspNet.Entities.Offre", "Offre")
                        .WithMany("Voyages")
                        .HasForeignKey("OffreIdOffre");

                    b.Navigation("Offre");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Agence", b =>
                {
                    b.Navigation("Offres");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Offre", b =>
                {
                    b.Navigation("Voyages");
                });

            modelBuilder.Entity("ApiAspNet.Entities.Gestionnaire", b =>
                {
                    b.Navigation("Agences");
                });
#pragma warning restore 612, 618
        }
    }
}
