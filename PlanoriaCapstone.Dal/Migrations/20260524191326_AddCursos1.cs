using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanoriaCapstone.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddCursos1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCursos",
                table: "ArchivosSubidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    IdCursos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.IdCursos);
                    table.ForeignKey(
                        name: "FK_Cursos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosSubidos_IdCursos",
                table: "ArchivosSubidos",
                column: "IdCursos");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_IdUsuario",
                table: "Cursos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosSubidos_Cursos_IdCursos",
                table: "ArchivosSubidos",
                column: "IdCursos",
                principalTable: "Cursos",
                principalColumn: "IdCursos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosSubidos_Cursos_IdCursos",
                table: "ArchivosSubidos");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosSubidos_IdCursos",
                table: "ArchivosSubidos");

            migrationBuilder.DropColumn(
                name: "IdCursos",
                table: "ArchivosSubidos");
        }
    }
}
