using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PasswordNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Participants",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Participants",
                newName: "Password");
        }
    }
}
