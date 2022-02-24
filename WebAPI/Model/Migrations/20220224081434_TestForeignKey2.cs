using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class TestForeignKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_User_UserId",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_UserId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bill");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Bill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerID",
                table: "Bill",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_User_CustomerID",
                table: "Bill",
                column: "CustomerID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_User_CustomerID",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_CustomerID",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Bill");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Bill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserId",
                table: "Bill",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_User_UserId",
                table: "Bill",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
