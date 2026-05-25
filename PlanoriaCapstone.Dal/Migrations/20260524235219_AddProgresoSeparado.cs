using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanoriaCapstone.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddProgresoSeparado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgresoFlashcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdFlashcard = table.Column<int>(type: "int", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    VecesRepasada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoFlashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresoFlashcards_Flashcards_IdFlashcard",
                        column: x => x.IdFlashcard,
                        principalTable: "Flashcards",
                        principalColumn: "IdFlashcard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresoFlashcards_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgresoQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdQuiz = table.Column<int>(type: "int", nullable: false),
                    Puntaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresoQuizzes_Quizzes_IdQuiz",
                        column: x => x.IdQuiz,
                        principalTable: "Quizzes",
                        principalColumn: "IdQuiz",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresoQuizzes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoFlashcards_IdFlashcard",
                table: "ProgresoFlashcards",
                column: "IdFlashcard");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoFlashcards_IdUsuario",
                table: "ProgresoFlashcards",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoQuizzes_IdQuiz",
                table: "ProgresoQuizzes",
                column: "IdQuiz");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoQuizzes_IdUsuario",
                table: "ProgresoQuizzes",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgresoFlashcards");

            migrationBuilder.DropTable(
                name: "ProgresoQuizzes");
        }
    }
}
