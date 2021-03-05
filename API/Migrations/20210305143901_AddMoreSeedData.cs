using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddMoreSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "Test 3 Test 3 Test", "https://via.placeholder.com/600x400", "Test 3", 499.50m },
                    { 4, "Test 4 Test 4 Test", "https://via.placeholder.com/600x400", "Test 4", 88m },
                    { 5, "Test 5 Test 5 Test", "https://via.placeholder.com/600x400", "Test 5", 201.60m },
                    { 6, "Test 6 Test 6 Test", "https://via.placeholder.com/600x400", "Test 6", 499m },
                    { 7, "Test 7 Test 7 Test", "https://via.placeholder.com/600x400", "Test 7", 301m },
                    { 8, "Test 8 Test 8 Test", "https://via.placeholder.com/600x400", "Test 8", 1113m },
                    { 9, "Test 9 Test 9 Test", "https://via.placeholder.com/600x400", "Test 9", 433.90m },
                    { 10, "Test 10 Test 10 Test", "https://via.placeholder.com/600x400", "Test 10", 349.22m },
                    { 11, "Test 11 Test 11 Test", "https://via.placeholder.com/600x400", "Test 11", 3039.99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
