using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanoriaCapstone.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GoogleId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "LOCAL"),
                    FotoPerfilUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RachaDias = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Puntos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Nivel = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosSubidos",
                columns: table => new
                {
                    IdArchivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlArchivo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TipoArchivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TamanoMB = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "ACTIVO")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosSubidos", x => x.IdArchivo);
                    table.ForeignKey(
                        name: "FK_ArchivosSubidos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    Accion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK_Auditorias_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EstadisticasIA",
                columns: table => new
                {
                    IdEstadistica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    TotalArchivosSubidos = table.Column<int>(type: "int", nullable: false),
                    TotalFlashcardsGeneradas = table.Column<int>(type: "int", nullable: false),
                    TotalQuizzesGenerados = table.Column<int>(type: "int", nullable: false),
                    PromedioPuntaje = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadisticasIA", x => x.IdEstadistica);
                    table.ForeignKey(
                        name: "FK_EstadisticasIA_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RachasUsuario",
                columns: table => new
                {
                    IdRacha = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiasConsecutivos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RachasUsuario", x => x.IdRacha);
                    table.ForeignKey(
                        name: "FK_RachasUsuario_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalisisIA",
                columns: table => new
                {
                    IdAnalisis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArchivo = table.Column<int>(type: "int", nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemasDetectados = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoProceso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "PROCESANDO"),
                    FechaAnalisis = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisisIA", x => x.IdAnalisis);
                    table.ForeignKey(
                        name: "FK_AnalisisIA_ArchivosSubidos_IdArchivo",
                        column: x => x.IdArchivo,
                        principalTable: "ArchivosSubidos",
                        principalColumn: "IdArchivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresoArchivos",
                columns: table => new
                {
                    IdProgresoArchivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdArchivo = table.Column<int>(type: "int", nullable: false),
                    FlashcardsCompletadas = table.Column<int>(type: "int", nullable: false),
                    FlashcardsTotales = table.Column<int>(type: "int", nullable: false),
                    QuizzesCompletados = table.Column<int>(type: "int", nullable: false),
                    QuizzesTotales = table.Column<int>(type: "int", nullable: false),
                    PorcentajeProgreso = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    UltimaSesion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoArchivos", x => x.IdProgresoArchivo);
                    table.ForeignKey(
                        name: "FK_ProgresoArchivos_ArchivosSubidos_IdArchivo",
                        column: x => x.IdArchivo,
                        principalTable: "ArchivosSubidos",
                        principalColumn: "IdArchivo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresoArchivos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    IdFlashcard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnalisis = table.Column<int>(type: "int", nullable: false),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelDificultad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VecesEstudiada = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.IdFlashcard);
                    table.ForeignKey(
                        name: "FK_Flashcards_AnalisisIA_IdAnalisis",
                        column: x => x.IdAnalisis,
                        principalTable: "AnalisisIA",
                        principalColumn: "IdAnalisis",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    IdQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnalisis = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.IdQuiz);
                    table.ForeignKey(
                        name: "FK_Quizzes_AnalisisIA_IdAnalisis",
                        column: x => x.IdAnalisis,
                        principalTable: "AnalisisIA",
                        principalColumn: "IdAnalisis",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialFlashcards",
                columns: table => new
                {
                    IdHistorialFlashcard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdFlashcard = table.Column<int>(type: "int", nullable: false),
                    Correcta = table.Column<bool>(type: "bit", nullable: false),
                    TiempoRespuestaSegundos = table.Column<int>(type: "int", nullable: true),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialFlashcards", x => x.IdHistorialFlashcard);
                    table.ForeignKey(
                        name: "FK_HistorialFlashcards_Flashcards_IdFlashcard",
                        column: x => x.IdFlashcard,
                        principalTable: "Flashcards",
                        principalColumn: "IdFlashcard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialFlashcards_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "HistorialQuizzes",
                columns: table => new
                {
                    IdHistorialQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdQuiz = table.Column<int>(type: "int", nullable: false),
                    Puntaje = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    CantidadCorrectas = table.Column<int>(type: "int", nullable: false),
                    CantidadIncorrectas = table.Column<int>(type: "int", nullable: false),
                    TiempoResolucionMinutos = table.Column<int>(type: "int", nullable: true),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialQuizzes", x => x.IdHistorialQuiz);
                    table.ForeignKey(
                        name: "FK_HistorialQuizzes_Quizzes_IdQuiz",
                        column: x => x.IdQuiz,
                        principalTable: "Quizzes",
                        principalColumn: "IdQuiz",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialQuizzes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "PreguntasQuiz",
                columns: table => new
                {
                    IdPreguntaQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuiz = table.Column<int>(type: "int", nullable: false),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpcionA = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OpcionB = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OpcionC = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OpcionD = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RespuestaCorrecta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explicacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntasQuiz", x => x.IdPreguntaQuiz);
                    table.ForeignKey(
                        name: "FK_PreguntasQuiz_Quizzes_IdQuiz",
                        column: x => x.IdQuiz,
                        principalTable: "Quizzes",
                        principalColumn: "IdQuiz",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisIA_IdArchivo",
                table: "AnalisisIA",
                column: "IdArchivo");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosSubidos_IdUsuario",
                table: "ArchivosSubidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_IdUsuario",
                table: "Auditorias",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_EstadisticasIA_IdUsuario",
                table: "EstadisticasIA",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_IdAnalisis",
                table: "Flashcards",
                column: "IdAnalisis");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialFlashcards_IdFlashcard",
                table: "HistorialFlashcards",
                column: "IdFlashcard");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialFlashcards_IdUsuario",
                table: "HistorialFlashcards",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialQuizzes_IdQuiz",
                table: "HistorialQuizzes",
                column: "IdQuiz");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialQuizzes_IdUsuario",
                table: "HistorialQuizzes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntasQuiz_IdQuiz",
                table: "PreguntasQuiz",
                column: "IdQuiz");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoArchivos_IdArchivo",
                table: "ProgresoArchivos",
                column: "IdArchivo");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoArchivos_IdUsuario",
                table: "ProgresoArchivos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_IdAnalisis",
                table: "Quizzes",
                column: "IdAnalisis");

            migrationBuilder.CreateIndex(
                name: "IX_RachasUsuario_IdUsuario",
                table: "RachasUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "EstadisticasIA");

            migrationBuilder.DropTable(
                name: "HistorialFlashcards");

            migrationBuilder.DropTable(
                name: "HistorialQuizzes");

            migrationBuilder.DropTable(
                name: "PreguntasQuiz");

            migrationBuilder.DropTable(
                name: "ProgresoArchivos");

            migrationBuilder.DropTable(
                name: "RachasUsuario");

            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "AnalisisIA");

            migrationBuilder.DropTable(
                name: "ArchivosSubidos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
