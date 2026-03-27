using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bibliotekssystem_notification_service.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewAndAdminLoanTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "BodyTemplate", "Subject", "Type" },
                values: new object[,]
                {
                    { 9, "En förändring har gjorts i hantera lån.", "Ändring i hantera lån", "AdminLoanChange" },
                    { 10, "Du har skapat en recension.", "Skapat en recension", "ReviewConfirmation" },
                    { 11, "Du har tagit bort en recension.", "Raderat en recension", "ReviewRemove" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
