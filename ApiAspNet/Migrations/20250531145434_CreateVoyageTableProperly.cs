using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiAspNet.Migrations
{
    /// <inheritdoc />
    public partial class CreateVoyageTableProperly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chauffeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chauffeurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flottes",
                columns: table => new
                {
                    IdFlotte = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeFlotte = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    MatriculeFlotte = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flottes", x => x.IdFlotte);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    CniClient = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CNIGestionnaire = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agences",
                columns: table => new
                {
                    IdAgence = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdresseAgence = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    NineaGestionnaire = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RccmGestionnaire = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdGestionnaire = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agences", x => x.IdAgence);
                    table.ForeignKey(
                        name: "FK_Agences_Users_IdGestionnaire",
                        column: x => x.IdGestionnaire,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateReservation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MontantReservation = table.Column<float>(type: "real", nullable: false),
                    StatutReservation = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offre",
                columns: table => new
                {
                    IdOffre = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptionOffre = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PrixOffre = table.Column<float>(type: "real", nullable: false),
                    DisponibiliteOffre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdAgence = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offre", x => x.IdOffre);
                    table.ForeignKey(
                        name: "FK_Offre_Agences_IdAgence",
                        column: x => x.IdAgence,
                        principalTable: "Agences",
                        principalColumn: "IdAgence",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voyage",
                columns: table => new
                {
                    IdVoyage = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Destination = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateDepart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateArrivee = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false),
                    OffreIdOffre = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyage", x => x.IdVoyage);
                    table.ForeignKey(
                        name: "FK_Voyage_Offre_OffreIdOffre",
                        column: x => x.OffreIdOffre,
                        principalTable: "Offre",
                        principalColumn: "IdOffre");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agences_IdGestionnaire",
                table: "Agences",
                column: "IdGestionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_IdAgence",
                table: "Offre",
                column: "IdAgence");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Voyage_OffreIdOffre",
                table: "Voyage",
                column: "OffreIdOffre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chauffeurs");

            migrationBuilder.DropTable(
                name: "Flottes");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Voyage");

            migrationBuilder.DropTable(
                name: "Offre");

            migrationBuilder.DropTable(
                name: "Agences");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
