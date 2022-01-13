using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "Path", "ProductId" },
                values: new object[,]
                {
                    { new Guid("475bffd1-7f4a-4dd1-a327-5f3dd48321b8"), "/Content/Hotdog.jpg", new Guid("5db89dfd-0615-453b-b9af-b39835d33c90") },
                    { new Guid("29e429d0-a84b-42e7-ad8b-75f7e538fcb3"), "/Content/cheeseCake.jpg", new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543") },
                    { new Guid("94b23910-deef-4cff-a426-ce687d235d58"), "/Content/cinnamon.jpg", new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b") },
                    { new Guid("d548613f-e19f-4cc3-992b-6851d696d617"), "/Content/summerSalad.jpg", new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e") },
                    { new Guid("9502fa21-fba5-4fe0-8624-d4f6f0812c0c"), "/Content/greekSalad.jpg", new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9") },
                    { new Guid("226a38af-16f7-436e-8575-aa034e81c540"), "/Content/Borzsch.png", new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd") },
                    { new Guid("52c5681a-d79b-4cb9-951c-168b19678d90"), "/Content/CabbagePie.png", new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85") },
                    { new Guid("ad0bd905-18a1-4731-856e-42895e85676e"), "/Content/ChickenWOK.png", new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9") },
                    { new Guid("f48560a1-715e-4d4e-be98-4c57f0ff7979"), "/Content/Cutlet.png", new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7") },
                    { new Guid("5d05cfdd-dae1-4837-a190-0e4e3d6a640e"), "/Content/Kharcho.png", new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6") },
                    { new Guid("c35416cf-ff64-4c98-bead-bfe824486f20"), "/Content/MashedPotato.png", new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12") },
                    { new Guid("9516e3dd-67a6-42cd-a13a-a198417700dd"), "/Content/MorseBerry.png", new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac") },
                    { new Guid("d24a2471-3c2f-4d2b-ac24-3f71d072299d"), "/Content/MorseBuckthorn.png", new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c") },
                    { new Guid("dbc848b0-e4b3-42df-9fd7-c54c6c4765df"), "/Content/Pancake.png", new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335") },
                    { new Guid("39278df9-2c10-49d4-ab41-fe93ca6a10f9"), "/Content/Rice.png", new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("061dde30-b3f1-45ef-b00b-d70e35d7d9ac"),
                column: "ImagePath",
                value: "/Content/MorseBerry.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11fac53d-bee3-4f63-b7e0-7a39d61336e9"),
                column: "ImagePath",
                value: "/Content/ChickenWOK.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4baf5f10-89c2-45ef-9e03-1981647b311b"),
                column: "ImagePath",
                value: "/Content/cinnamon.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d031949-6efb-4084-b2ce-7c01622ec7d7"),
                column: "ImagePath",
                value: "/Content/Cutlet.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5db89dfd-0615-453b-b9af-b39835d33c90"),
                column: "ImagePath",
                value: "/Content/Hotdog.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7d7e9765-9be5-4c3d-8ca6-868dfa887543"),
                column: "ImagePath",
                value: "/Content/cheeseCake.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9bb66902-d72c-422e-b18a-80d1ce411c12"),
                column: "ImagePath",
                value: "/Content/MashedPotato.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d87c586-08bc-4af8-8604-2a1f44931a6e"),
                column: "ImagePath",
                value: "/Content/summerSalad.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9eb081b4-7e20-4e90-b5ab-f94d75e82ac9"),
                column: "ImagePath",
                value: "/Content/greekSalad.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2a5d4ad-73b8-4b1a-9164-9d37758765bd"),
                column: "ImagePath",
                value: "/Content/Borzsch.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b46a8ceb-650c-46fb-a9d5-1d081ce4e335"),
                column: "ImagePath",
                value: "/Content/Pancake.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b7efb40a-4090-46f1-896a-b84fd7b4fa85"),
                column: "ImagePath",
                value: "/Content/CabbagePie.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c589b5b2-ceda-439a-beed-66fc9ef9b52c"),
                column: "ImagePath",
                value: "/Content/MorseBuckthorn.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("db5c6a11-7b3a-463e-add5-01b628cb30e6"),
                column: "ImagePath",
                value: "/Content/Kharcho.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dcef4e05-4a34-4a26-99b0-592bf38174cb"),
                column: "ImagePath",
                value: "/Content/Rice.png");
        }
    }
}
