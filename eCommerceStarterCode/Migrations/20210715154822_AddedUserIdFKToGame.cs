using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceStarterCode.Migrations
{
    public partial class AddedUserIdFKToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5482eba0-6113-4431-9418-0123162fa56f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae42e6c0-a6c9-46f8-b7f8-6c1f27cc1497");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2620e7d4-bc24-4e8e-868c-b1161e55f1e0", "45f88d4b-76cd-47da-b959-e3fd244e1c61", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a0866ee-c6ad-445c-a8b0-abbf04959ade", "99dbdd98-4133-4c69-8978-d6a9365b17c1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2620e7d4-bc24-4e8e-868c-b1161e55f1e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a0866ee-c6ad-445c-a8b0-abbf04959ade");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Games");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae42e6c0-a6c9-46f8-b7f8-6c1f27cc1497", "1ba5561a-f020-4eb3-ac63-83ec3bda1a0c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5482eba0-6113-4431-9418-0123162fa56f", "8edf1594-2530-4b3c-b3ba-9fff4246328d", "Admin", "ADMIN" });
        }
    }
}
