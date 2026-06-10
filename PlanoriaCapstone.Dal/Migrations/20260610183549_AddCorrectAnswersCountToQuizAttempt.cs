using Microsoft.EntityFrameworkCore.Migrations;
using UglyToad.PdfPig;
#nullable disable

namespace PlanoriaCapstone.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectAnswersCountToQuizAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersCount",
                table: "QuizAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswersCount",
                table: "QuizAttempts");
        }
    }
}
