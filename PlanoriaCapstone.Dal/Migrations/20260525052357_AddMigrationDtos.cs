using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanoriaCapstone.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationDtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCursos",
                table: "Cursos",
                newName: "IdCurso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCurso",
                table: "Cursos",
                newName: "IdCursos");
        }
    }
}
