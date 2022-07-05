using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Remove_UserRole_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Image_ImageID",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteList_Product_ProductID",
                table: "FavoriteList");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_Image_ImageID",
                table: "NewsImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_News_NewsID",
                table: "NewsImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Color_ColorID",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Product_ProductID",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Image_ImageID",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductImage",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "ProductImage",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductID",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductColor",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "ProductColor",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColor_ProductID",
                table: "ProductColor",
                newName: "IX_ProductColor_ProductId");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Product",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandID",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.RenameColumn(
                name: "NewsID",
                table: "NewsImage",
                newName: "NewsId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "NewsImage",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_NewsID",
                table: "NewsImage",
                newName: "IX_NewsImage_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_ImageID",
                table: "NewsImage",
                newName: "IX_NewsImage_ImageId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "FavoriteList",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "FavoriteList",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteList_ProductID",
                table: "FavoriteList",
                newName: "IX_FavoriteList_ProductId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "Brand",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_ImageID",
                table: "Brand",
                newName: "IX_Brand_ImageId");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "Bill",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Bill",
                newName: "CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Image_ImageId",
                table: "Brand",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteList_Product_ProductId",
                table: "FavoriteList",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_Image_ImageId",
                table: "NewsImage",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Color_ColorId",
                table: "ProductColor",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Image_ImageId",
                table: "ProductImage",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Image_ImageId",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteList_Product_ProductId",
                table: "FavoriteList");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_Image_ImageId",
                table: "NewsImage");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Color_ColorId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Image_ImageId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImage",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ProductImage",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductColor",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "ProductColor",
                newName: "ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColor_ProductId",
                table: "ProductColor",
                newName: "IX_ProductColor_ProductID");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Product",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                newName: "IX_Product_BrandID");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "NewsImage",
                newName: "NewsID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "NewsImage",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage",
                newName: "IX_NewsImage_NewsID");

            migrationBuilder.RenameIndex(
                name: "IX_NewsImage_ImageId",
                table: "NewsImage",
                newName: "IX_NewsImage_ImageID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "FavoriteList",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "FavoriteList",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteList_ProductId",
                table: "FavoriteList",
                newName: "IX_FavoriteList_ProductID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Brand",
                newName: "ImageID");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_ImageId",
                table: "Brand",
                newName: "IX_Brand_ImageID");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Bill",
                newName: "StatusID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Bill",
                newName: "CustomerID");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Image_ImageID",
                table: "Brand",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteList_Product_ProductID",
                table: "FavoriteList",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_Image_ImageID",
                table: "NewsImage",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_News_NewsID",
                table: "NewsImage",
                column: "NewsID",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandID",
                table: "Product",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Color_ColorID",
                table: "ProductColor",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Product_ProductID",
                table: "ProductColor",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Image_ImageID",
                table: "ProductImage",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductID",
                table: "ProductImage",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
