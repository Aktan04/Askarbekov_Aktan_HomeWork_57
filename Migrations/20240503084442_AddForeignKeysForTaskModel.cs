using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hw57.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysForTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutorName",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExecutorId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatorId",
                table: "Tasks",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_ExecutorId",
                table: "Tasks",
                column: "ExecutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_ExecutorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorName",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
