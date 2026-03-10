using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bibliotekssystem_notification_service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemplatesMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyTemplate",
                value: "Hej! Ditt lånade material ska lämnas  tillbaka senast {DueDate}.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "BodyTemplate",
                value: "Ditt lånade material skulle ha lämnats  tillbaka {DueDate}. Vänligen återlämna snarast.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "BodyTemplate",
                value: "Det du reserverade är nu  tillgänglig för upphämtning.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "BodyTemplate",
                value: "Du har lånat ett objekt.  Återlämningsdatum: {DueDate}.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyTemplate",
                value: "Hej! Din lånade bok ska lämnas  tillbaka senast {DueDate}.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "BodyTemplate",
                value: "Din lånade bok skulle ha lämnats  tillbaka {DueDate}. Vänligen återlämna snarast.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "BodyTemplate",
                value: "Boken du reserverade är nu  tillgänglig för upphämtning.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 4,
                column: "BodyTemplate",
                value: "Du har lånat en bok.  Återlämningsdatum: {DueDate}.");
        }
    }
}
