using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Sensors_SensorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_ClientId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Users_ClientId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ClientId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SensorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Tickets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets",
                newName: "IX_Tickets_UserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Sensors",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_ClientId",
                table: "Sensors",
                newName: "IX_Sensors_UserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Categories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ClientId",
                table: "Categories",
                newName: "IX_Categories_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "SensorCategories",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorCategories", x => new { x.SensorId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_SensorCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensorCategories_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorCategories_CategoryId",
                table: "SensorCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Users_UserId",
                table: "Sensors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Users_UserId",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SensorCategories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tickets",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                newName: "IX_Tickets_ClientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sensors",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors",
                newName: "IX_Sensors_ClientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Categories",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                newName: "IX_Categories_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SensorId",
                table: "Categories",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Sensors_SensorId",
                table: "Categories",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_ClientId",
                table: "Categories",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Users_ClientId",
                table: "Sensors",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
