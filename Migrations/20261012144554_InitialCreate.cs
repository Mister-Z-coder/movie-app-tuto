using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Realisateur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Synopsis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DateSortie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DureeMinutes = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<int>(type: "int", nullable: false),
                    AfficheUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateAjout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Nom" },
                values: new object[,]
                {
                    { 1, "Futurs possibles, espace, technologies.", "Science-fiction" },
                    { 2, "Rythme soutenu,combats et poursuites.", "Action" },
                    { 3, "Suspense et tension.", "Thriller" },
                    { 4, "Films d'animation pour tous.", "Animation" },
                    { 5, "Histoires fortes en émotions.", "Drame" },
                    { 6, "Faits et figures de l'histoire.", "Historique" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "AfficheUrl", "DateAjout", "DateSortie", "DureeMinutes", "GenreId", "Note", "Realisateur", "Synopsis", "Titre" },
                values: new object[,]
                {
                    { 1, "https://placehold.co/400x600?text=Inception", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 148, 1, 9, "Christopher Nolan", "Un voleur spécialisé dans l'extraction de secrets au cœur des rêves reçoit une mission inverse : implanter une idée.", "Inception" },
                    { 2, "https://placehold.co/400x600? text = Interstellar", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 169, 1, 9, "Christopher Nolan", "Alors que la Terre se meurt, un groupe d'astronautes traverse un trou de ver pour trouver une nouvelle planète.", "Interstellar" },
                    { 3, "https://placehold.co/400x600?text = Black + Panther", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 134, 2, 8, "Ryan Coogler", "Le nouveau roi du Wakanda doit défendre son trône et son peuple face à un rival venu de l'extérieur.", "Black Panther" },
                    { 4, "https://placehold.co/400x600?text=Parasite", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 132, 3, 9, "Bong Joonho", "Une famille pauvre s'infiltre peu à peu au service d'une famille riche, jusqu'au point de rupture.", "Parasite" },
                    { 5, "https://placehold.co/400x600?text=Toy+Story", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 81, 4, 8, "John Lasseter", "Les jouets d'un enfant prennent vie dès qu'ilquitte la pièce; l'arrivée d'un nouveau jouet bouscule tout.", "Toy Story" },
                    { 6, "https://placehold.co/400x600?text=Titanic", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 194, 5, 8, "James Cameron", "Une histoire d'amour à bord du paquebot réputé insubmersible,lors de sa traversée de 1912.", "Titanic" },
                    { 7, "https://placehold.co/400x600?text=Lumumba", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 115, 6, 8, "Raoul Peck", "Le parcours de Patrice Lumumba, de l'indépendance du Congo en 1960 à son assassinat.", "Lumumba" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Nom",
                table: "Genres",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
