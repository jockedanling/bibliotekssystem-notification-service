using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bibliotekssystem_notification_service.Migrations
{
    /// <inheritdoc />
    public partial class AdminNotificationsTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "BodyTemplate", "Subject", "Type" },
                values: new object[,]
                {
                    { 6, "En förändring har gjorts i hantera katalog.", "Ändring i hantera katalog", "AdminCatalogChange" },
                    { 7, "En förändring har gjorts i hantera användare.", "Ändring i hantera användare", "AdminUserChange" },
                    { 8, "En förändring har gjorts i hantera recensioner.", "Ändring i hantera recensioner", "AdminReviewChange" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
