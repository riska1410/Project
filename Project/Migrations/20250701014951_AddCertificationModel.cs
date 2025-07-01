using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certifications_TrainingId",
                table: "Certifications");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_TrainingId",
                table: "Certifications",
                column: "TrainingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certifications_TrainingId",
                table: "Certifications");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_TrainingId",
                table: "Certifications",
                column: "TrainingId");
        }
    }
}
