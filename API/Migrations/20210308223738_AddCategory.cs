using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b2a1dfc-22b1-4029-8229-6833d63c3c71"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("01ba1e57-5264-42cb-a071-9de6afdb1efd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0f41234e-16f3-41b3-a33e-c1725df153e3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3eb26bc1-bb62-4f8b-b121-75132d547a4f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6198a31e-fed9-41cb-843b-75a15df3abb2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6b15d5d6-badf-4475-a358-999d125dc9ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6b17f38d-ea52-4ca1-9de0-23864cca988a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("81a3aa6d-8069-4149-80aa-3d0bd4bd6dcb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8d60f1d1-54b2-4d20-b7b7-2333d05e985b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a87c9c24-4100-4460-bea3-5479c9de7366"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b44a221e-d4af-4080-83a2-d02e79688852"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c044ebfa-c63b-4cad-a206-99414354d547"));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("f931582f-dcf7-4ef0-a821-b238fea4a39f"), "", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0a60cbcf-5a19-4928-b247-583414f59e96"), null, "Test Test Test", "https://via.placeholder.com/600x400", "Test", 39m },
                    { new Guid("6554f526-b1f0-4902-8143-d7598cc8b2c8"), null, "Test 2 Test 2 Test", "https://via.placeholder.com/600x400", "Test 2", 339m },
                    { new Guid("d62e9666-7b82-4b8d-bedb-f71b14f4cf88"), null, "Test 3 Test 3 Test", "https://via.placeholder.com/600x400", "Test 3", 499.50m },
                    { new Guid("ca06e259-85aa-4dcb-9085-e81cde0701bd"), null, "Test 4 Test 4 Test", "https://via.placeholder.com/600x400", "Test 4", 88m },
                    { new Guid("c6c64d32-951d-45c4-89b6-0c0ebd4e1cc1"), null, "Test 5 Test 5 Test", "https://via.placeholder.com/600x400", "Test 5", 201.60m },
                    { new Guid("1e47bd63-680e-4aba-86a6-db5afed8163f"), null, "Test 6 Test 6 Test", "https://via.placeholder.com/600x400", "Test 6", 499m },
                    { new Guid("d37c967d-215e-4625-885b-6095e4d7feed"), null, "Test 7 Test 7 Test", "https://via.placeholder.com/600x400", "Test 7", 301m },
                    { new Guid("1d1f181b-ece6-4319-88e5-662ae58aa08e"), null, "Test 8 Test 8 Test", "https://via.placeholder.com/600x400", "Test 8", 1113m },
                    { new Guid("3bf77edf-1f55-4648-9930-aaea459bdf80"), null, "Test 9 Test 9 Test", "https://via.placeholder.com/600x400", "Test 9", 433.90m },
                    { new Guid("7a7c778b-36ad-4b5b-a7b7-9675eada973b"), null, "Test 10 Test 10 Test", "https://via.placeholder.com/600x400", "Test 10", 349.22m },
                    { new Guid("33775884-cd59-4842-86ab-7f4e9766d54f"), null, "Test 11 Test 11 Test", "https://via.placeholder.com/600x400", "Test 11", 3039.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f931582f-dcf7-4ef0-a821-b238fea4a39f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0a60cbcf-5a19-4928-b247-583414f59e96"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1d1f181b-ece6-4319-88e5-662ae58aa08e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1e47bd63-680e-4aba-86a6-db5afed8163f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33775884-cd59-4842-86ab-7f4e9766d54f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3bf77edf-1f55-4648-9930-aaea459bdf80"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6554f526-b1f0-4902-8143-d7598cc8b2c8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7a7c778b-36ad-4b5b-a7b7-9675eada973b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c6c64d32-951d-45c4-89b6-0c0ebd4e1cc1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ca06e259-85aa-4dcb-9085-e81cde0701bd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d37c967d-215e-4625-885b-6095e4d7feed"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d62e9666-7b82-4b8d-bedb-f71b14f4cf88"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1b2a1dfc-22b1-4029-8229-6833d63c3c71"), "", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("6b15d5d6-badf-4475-a358-999d125dc9ca"), "Test Test Test", "https://via.placeholder.com/600x400", "Test", 39m },
                    { new Guid("6b17f38d-ea52-4ca1-9de0-23864cca988a"), "Test 2 Test 2 Test", "https://via.placeholder.com/600x400", "Test 2", 339m },
                    { new Guid("01ba1e57-5264-42cb-a071-9de6afdb1efd"), "Test 3 Test 3 Test", "https://via.placeholder.com/600x400", "Test 3", 499.50m },
                    { new Guid("6198a31e-fed9-41cb-843b-75a15df3abb2"), "Test 4 Test 4 Test", "https://via.placeholder.com/600x400", "Test 4", 88m },
                    { new Guid("a87c9c24-4100-4460-bea3-5479c9de7366"), "Test 5 Test 5 Test", "https://via.placeholder.com/600x400", "Test 5", 201.60m },
                    { new Guid("c044ebfa-c63b-4cad-a206-99414354d547"), "Test 6 Test 6 Test", "https://via.placeholder.com/600x400", "Test 6", 499m },
                    { new Guid("8d60f1d1-54b2-4d20-b7b7-2333d05e985b"), "Test 7 Test 7 Test", "https://via.placeholder.com/600x400", "Test 7", 301m },
                    { new Guid("3eb26bc1-bb62-4f8b-b121-75132d547a4f"), "Test 8 Test 8 Test", "https://via.placeholder.com/600x400", "Test 8", 1113m },
                    { new Guid("81a3aa6d-8069-4149-80aa-3d0bd4bd6dcb"), "Test 9 Test 9 Test", "https://via.placeholder.com/600x400", "Test 9", 433.90m },
                    { new Guid("0f41234e-16f3-41b3-a33e-c1725df153e3"), "Test 10 Test 10 Test", "https://via.placeholder.com/600x400", "Test 10", 349.22m },
                    { new Guid("b44a221e-d4af-4080-83a2-d02e79688852"), "Test 11 Test 11 Test", "https://via.placeholder.com/600x400", "Test 11", 3039.99m }
                });
        }
    }
}
